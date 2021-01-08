using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
  public class RolNuevo
  {
    public class RolNuevoRequest : IRequest
    {
      public string Nombre { get; set; }
    }

    public class RolNuevoValidator : AbstractValidator<RolNuevoRequest>
    {
      public RolNuevoValidator()
      {
        RuleFor(x => x.Nombre).NotEmpty();
      }
    }

    public class RolNuevoHandler : IRequestHandler<RolNuevoRequest>
    {
      private readonly RoleManager<IdentityRole> _roleManager;
      public RolNuevoHandler(RoleManager<IdentityRole> roleManager)
      {
        _roleManager = roleManager;
      }
      public async Task<Unit> Handle(RolNuevoRequest request, CancellationToken cancellationToken)
      {
        var rol = await _roleManager.FindByNameAsync(request.Nombre);
        if (rol != null) throw new ExceptionHandling(HttpStatusCode.BadRequest, new { message = "Ya existe el rol" });
        var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));
        if (resultado.Succeeded) return Unit.Value;
        throw new Exception("No se pudo insertar el rol");
      }
    }
  }
}