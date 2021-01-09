using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
  public class CurrentUser
  {
    public class CurrentUserRequest : IRequest<UserData>
    {

    }

    public class CurrentUserHandler : IRequestHandler<CurrentUserRequest, UserData>
    {
      private readonly UserManager<User> _userManager;
      private readonly IJwtGenerator _jwtGenerator;
      private readonly IUserSession _userSession;
      public CurrentUserHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
      {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _userSession = userSession;
      }

      public async Task<UserData> Handle(CurrentUserRequest request, CancellationToken cancellationToken)
      {
        var user = await _userManager.FindByNameAsync(_userSession.ObtainUserSession());
        var listaRoles = await _userManager.GetRolesAsync(user);
        return new UserData
        {
          NombreCompleto = user.NombreCompleto,
          Username = user.UserName,
          Token = _jwtGenerator.CrearToken(user, new List<string>(listaRoles)),
          Imagen = null,
          Email = user.Email
        };
      }
    }
  }
}