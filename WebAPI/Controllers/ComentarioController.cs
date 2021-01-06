using System;
using System.Threading.Tasks;
using Aplicacion.Comentarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ComentarioController : BaseController
    {
        // http://localhost:5000/api/Comentario
        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromBody] Nuevo.NuevoComentarioRequest curso)
        {
            return await Mediator.Send(curso);
        }

        // http://localhost:5000/api/Comentario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarComentarioRequest {ComentarioId = id});
        }
    }
}