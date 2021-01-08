using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Paginacion;

namespace WebAPI.Controllers
{
  // http://localhost:5000/api/Cursos
  [Route("api/[controller]")]
  [ApiController]
  public class CursosController : BaseController
  {
    // http://localhost:5000/api/Cursos
    [HttpGet]
    public async Task<ActionResult<List<CursoDto>>> Get()
    {
      return await Mediator.Send(new Consulta.ListaCursos());
    }

    // http://localhost:5000/api/Cursos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CursoDto>> GetById(Guid id)
    {
      return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
    }

    // http://localhost:5000/api/Cursos
    [HttpPost]
    public async Task<ActionResult<Unit>> Crear(Nuevo.NuevoCursoRequest curso)
    {
      return await Mediator.Send(curso);
    }

    // http://localhost:5000/api/Cursos/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> Actualizar(Guid id, Editar.EditarCursoRequest curso)
    {
      curso.CursoId = id;
      return await Mediator.Send(curso);
    }

    // http://localhost:5000/api/Cursos/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid id)
    {
      return await Mediator.Send(new Eliminar.EliminarCursoRequest { CursoId = id });
    }

    // http://localhost:5000/api/Cursos/report
    [HttpPost("report")]
    public async Task<ActionResult<PaginacionModel>> Report([FromBody] PaginacionCurso.PaginacionCursoRequest paginacionCursoRequest)
    {
      return await Mediator.Send(paginacionCursoRequest);
    }
  }
}