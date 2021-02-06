using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class CurrentUser
    {
        public class CurrentUserRequest : IRequest<UserData>
        {

        }

        public class CurrentUserHandler : IRequestHandler<CurrentUserRequest, UserData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserSession _userSession;
            public CurrentUserHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession, CursosOnlineContext context)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _userSession = userSession;
                _context = context;
            }

            public async Task<UserData> Handle(CurrentUserRequest request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSession.ObtainUserSession());
                var listaRoles = await _userManager.GetRolesAsync(user);
                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(user.Id)).FirstOrDefaultAsync();
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
                        Username = user.UserName,
                        Token = _jwtGenerator.CrearToken(user, new List<string>(listaRoles)),
                        Imagen = null,
                        ImagenPerfil = imagenCliente,
                        Email = user.Email
                    };
                }else
                {
                    return new UserData
                    {
                        NombreCompleto = user.NombreCompleto,
                        Username = user.UserName,
                        Token = _jwtGenerator.CrearToken(user, new List<string>(listaRoles)),
                        ImagenPerfil = null,
                        Imagen = null,
                        Email = user.Email
                    };
                }
            }
        }
    }
}