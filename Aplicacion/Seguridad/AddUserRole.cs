using System;
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
  public class AddUserRole
  {
    public class AddUserRoleRequest : IRequest
    {
      public string Username { get; set; }
      public string RolNombre { get; set; }
    }

    public class AddUserRoleValidator : AbstractValidator<AddUserRoleRequest>
    {
      public AddUserRoleValidator()
      {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.RolNombre).NotEmpty();
      }
    }

    public class AddUserRoleHandler : IRequestHandler<AddUserRoleRequest>
    {
      private readonly RoleManager<IdentityRole> _roleManager;
      private readonly UserManager<User> _userManager;
      public AddUserRoleHandler(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
      {
        _roleManager = roleManager;
        _userManager = userManager;
      }
      public async Task<Unit> Handle(AddUserRoleRequest request, CancellationToken cancellationToken)
      {
        var role = await _roleManager.FindByNameAsync(request.RolNombre) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "El rol no se encontró"});
        var userIdentity = await _userManager.FindByNameAsync(request.Username) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "El usuario no se encontró"});

        var resultado = await _userManager.AddToRoleAsync(userIdentity, request.RolNombre);

        if(resultado.Succeeded) return Unit.Value;
        throw new Exception("No se pudo agregar el rol al usuario");
      }
    }
  }
}