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
  public class RolEliminar
  {
    public class RolEliminarRequest : IRequest
    {
      public string Nombre { get; set; }
    }

    public class RolEliminarValidator : AbstractValidator<RolEliminarRequest>
    {
      public RolEliminarValidator()
      {
        RuleFor(x => x.Nombre).NotEmpty();
      }
    }

    public class RolNuevoHandler : IRequestHandler<RolEliminarRequest>
    {
      private readonly RoleManager<IdentityRole> _roleManager;
      public RolNuevoHandler(RoleManager<IdentityRole> roleManager)
      {
        _roleManager = roleManager;
      }
      public async Task<Unit> Handle(RolEliminarRequest request, CancellationToken cancellationToken)
      {
        var rol = await _roleManager.FindByNameAsync(request.Nombre);
        if (rol == null) throw new ExceptionHandling(HttpStatusCode.BadRequest, new { message = "No existe el rol" });

        var resultado = await _roleManager.DeleteAsync(rol);
        if (resultado.Succeeded) return Unit.Value;

        throw new Exception("No se pudo eliminar el rol");
      }
    }

  }
}