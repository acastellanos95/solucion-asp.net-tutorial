using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Instructor;

namespace WebAPI.Controllers
{
  public class InstructorController : BaseController
  {
    // http://localhost:5000/api/Instructor
    [HttpGet]
    public async Task<ActionResult<List<InstructorModel>>> GetInstructores()
    {
      return await Mediator.Send(new Consulta.Lista());
    }

    // http://localhost:5000/api/Instructor/id
    [HttpGet("{id}")]
    public async Task<ActionResult<InstructorModel>> GetByIdInstructor(Guid id)
    {
      return await Mediator.Send(new ConsultaId.ConsultaIdInstructorRequest{InstructorId = id});
    }

    // http://localhost:5000/api/Instructor
    [HttpPost]
    public async Task<ActionResult<Unit>> NewInstructor([FromBody] Nuevo.NuevoInstructorRequest request)
    {
      return await Mediator.Send(request);
    }

    // http://localhost:5000/api/Instructor/id
    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> EditInstructor(Guid id, [FromBody] Editar.EditarInstructorRequest request)
    {
      request.InstructorId = id;
      return await Mediator.Send(request);
    }

    // http://localhost:5000/api/Instructor/id
    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> DeleteInstructor(Guid id)
    {
      return await Mediator.Send(new Eliminar.EliminarInstructorRequest{InstructorId = id});
    }
  }
}