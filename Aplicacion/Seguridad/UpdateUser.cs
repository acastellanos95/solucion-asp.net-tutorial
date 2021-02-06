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
    public class UpdateUser
    {
        public class UpdateUserRequest : IRequest<UserData>
        {
            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            public ImagenGeneral ImagenPerfil { get; set; }
        }

        public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
        {
            public UpdateUserValidator()
            {
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IPasswordHasher<User> _passwordHasher;
            public UpdateUserHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator, CursosOnlineContext context, IPasswordHasher<User> passwordHasher)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserData> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
            {
                var userIdentity = await _userManager.FindByNameAsync(request.Username) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el username" });
                var result = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();

                if (result) throw new ExceptionHandling(HttpStatusCode.InternalServerError, new { message = "Este email pertenece a otro usuario" });
                if (request.ImagenPerfil != null)
                {
                    var resultadoImagen = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(userIdentity.Id)).FirstOrDefaultAsync();
                    if (resultadoImagen == null)
                    {
                        var Imagen = new Documento
                        {
                            Contenido = Convert.FromBase64String(request.ImagenPerfil.Data),
                            Nombre = request.ImagenPerfil.Nombre,
                            Extension = request.ImagenPerfil.Extension,
                            ObjetoReferencia = new Guid(userIdentity.Id),
                            DocumentoId = Guid.NewGuid(),
                            FechaCreacion = DateTime.UtcNow
                        };
                        _context.Documento.Add(Imagen); 
                    }
                    else
                    {
                        resultadoImagen.Contenido = Convert.FromBase64String(request.ImagenPerfil.Data);
                        resultadoImagen.Nombre = request.ImagenPerfil.Nombre;
                        resultadoImagen.Extension = request.ImagenPerfil.Extension;
                    }
                }

                userIdentity.NombreCompleto = request.NombreCompleto;
                userIdentity.PasswordHash = _passwordHasher.HashPassword(userIdentity, request.Password);
                userIdentity.Email = request.Email;

                var roles = await _userManager.GetRolesAsync(userIdentity);
                var userResult = await _userManager.UpdateAsync(userIdentity);
                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(userIdentity.Id)).FirstAsync();
                ImagenGeneral imagenGeneral = null;
                if(imagenPerfil != null)
                {
                    imagenGeneral = new ImagenGeneral
                    {
                        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                        Nombre = imagenPerfil.Nombre,
                        Extension = imagenPerfil.Extension
                    };
                }

                if (userResult.Succeeded) return new UserData
                {
                    NombreCompleto = userIdentity.NombreCompleto,
                    Username = userIdentity.UserName,
                    Email = userIdentity.Email,
                    Token = _jwtGenerator.CrearToken(userIdentity, new List<string>(roles)),
                    ImagenPerfil = imagenGeneral
                };

                throw new Exception("No se pudo actualizar el usuario");
            }
        }

    }
}