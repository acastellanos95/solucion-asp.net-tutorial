using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  public class RolController : BaseController
  {
    // http://localhost:5000/api/Rol/crear
    [HttpPost("crear")]
    public async Task<ActionResult<Unit>> Create([FromBody] RolNuevo.RolNuevoRequest request)
    {
      return await Mediator.Send(request);
    }
    // http://localhost:5000/api/Rol/crear
    [HttpDelete("eliminar")]
    public async Task<ActionResult<Unit>> Delete([FromBody] RolEliminar.RolEliminarRequest request)
    {
      return await Mediator.Send(request);
    }
  }
}