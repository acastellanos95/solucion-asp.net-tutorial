using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
  public class UserRoleLista
  {
    public class UserRoleListaRequest : IRequest<List<string>>
    {
      public string Username { get; set; }
    }

    public class UserRoleListaValidator : AbstractValidator<UserRoleListaRequest>
    {
      public UserRoleListaValidator()
      {
        RuleFor(x => x.Username).NotEmpty();
      }
    }

    public class DeleteUserRoleHandler : IRequestHandler<UserRoleListaRequest, List<string>>
    {
      private readonly RoleManager<IdentityRole> _roleManager;
      private readonly UserManager<User> _userManager;
      public DeleteUserRoleHandler(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
      {
        _roleManager = roleManager;
        _userManager = userManager;
      }
      public async Task<List<string>> Handle(UserRoleListaRequest request, CancellationToken cancellationToken)
      {
        var userIdentity = await _userManager.FindByNameAsync(request.Username) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "El usuario no se encontró" });
        var roles = await _userManager.GetRolesAsync(userIdentity);
        return new List<string>(roles);
      }
    }
  }
}