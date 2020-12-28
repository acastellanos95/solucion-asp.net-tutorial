using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    // http://localhost:5000/api/Cursos
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(int id)
        {
            return await _mediator.Send(new ConsultaId.CursoUnico{Id=id});
        }

        // http://localhost:5000/api/Cursos
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.NuevoCursoRequest curso)
        {
            return await _mediator.Send(curso);
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Actualizar(int id, Editar.EditarCursoRequest curso)
        {
            curso.CursoId = id;
            return await _mediator.Send(curso);
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await _mediator.Send(new Eliminar.EliminarCursoRequest{CursoId=id});
        }
    }
}