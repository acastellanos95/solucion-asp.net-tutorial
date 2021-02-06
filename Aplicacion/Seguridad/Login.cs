using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ErrorHandling;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class LoginRequest : IRequest<UserData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginValidator : AbstractValidator<LoginRequest>
        {
            public LoginValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class LoginHandler : IRequestHandler<LoginRequest, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly CursosOnlineContext _context;

            public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator generator, CursosOnlineContext context)
            {
                _jwtGenerator = generator;
                _userManager = userManager;
                _signInManager = signInManager;
                _context = context;
            }

            public async Task<UserData> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new ExceptionHandling(HttpStatusCode.Unauthorized, new { message = "No se encontró el mail" });
                }
                var res = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                var listaRoles = await _userManager.GetRolesAsync(user);
                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(user.Id)).FirstOrDefaultAsync();

                if (res.Succeeded)
                {
                    if (imagenPerfil != null)
                    {
                        var imagenCliente = new ImagenGeneral
                        {
                            Data = Convert.ToBase64String(imagenPerfil.Contenido),
                            Extension = imagenPerfil.Extension,
                            Nombre = imagenPerfil.Nombre
                        };
                        return new UserData
                        {
                            NombreCompleto = user.NombreCompleto,
                            Token = _jwtGenerator.CrearToken(user, new List<string>(listaRoles)),
                            Username = user.UserName,
                            Email = user.Email,
                            Imagen = null,
                            ImagenPerfil = imagenCliente
                        };
                    }
                    else
                    {
                        return new UserData
                        {
                            NombreCompleto = user.NombreCompleto,
                            Token = _jwtGenerator.CrearToken(user, new List<string>(listaRoles)),
                            Username = user.UserName,
                            Email = user.Email,
                            Imagen = null,
                        };
                    }
                }
                throw new ExceptionHandling(HttpStatusCode.Unauthorized);
            }
        }
    }
}