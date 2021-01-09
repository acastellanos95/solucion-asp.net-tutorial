using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  public class RolController : BaseController
  {
    // http://localhost:5000/api/Rol/
    [HttpGet("lista")]
    public async Task<ActionResult<List<IdentityRole>>> GetAll()
    {
      return await Mediator.Send( new RolLista.RolListaRequest());
    }

    // http://localhost:5000/api/Rol/{username}
    [HttpGet("{username}")]
    public async Task<ActionResult<List<string>>> GetListaRolesUsuario(string username)
    {
      return await Mediator.Send(new UserRoleLista.UserRoleListaRequest{ Username = username });
    }

    // http://localhost:5000/api/Rol/crear
    [HttpPost("crear")]
    public async Task<ActionResult<Unit>> Create([FromBody] RolNuevo.RolNuevoRequest request)
    {
      return await Mediator.Send(request);
    }

    // http://localhost:5000/api/Rol/agregarRoleUsuario
    [HttpPost("agregarRoleUsuario")]
    public async Task<ActionResult<Unit>> AddUserRole([FromBody] AddUserRole.AddUserRoleRequest request)
    {
      return await Mediator.Send(request);
    }

    // http://localhost:5000/api/Rol/eliminar
    [HttpDelete("eliminar")]
    public async Task<ActionResult<Unit>> Delete([FromBody] RolEliminar.RolEliminarRequest request)
    {
      return await Mediator.Send(request);
    }

    // http://localhost:5000/api/Rol/eliminarRoleUsuario
    [HttpDelete("eliminarRoleUsuario")]
    public async Task<ActionResult<Unit>> DeleteUserRole([FromBody] DeleteUserRole.DeleteUserRoleRequest request)
    {
      return await Mediator.Send(request);
    }
  }
}