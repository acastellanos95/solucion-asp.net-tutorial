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

        // http://localhost:5000/api/Instructor
        [HttpPost]
        public async Task<ActionResult<Unit>> NewInstructor(Nuevo.NuevoInstructorRequest request)
        {
            return await Mediator.Send(request);
        }
    }
}