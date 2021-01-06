using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>>
        {        }

    public class ListaHandler : IRequestHandler<Lista, List<InstructorModel>>
    {
        private readonly IInstructor _instructorRepositorio;
        public ListaHandler(IInstructor instructorRepositorio)
        {
            _instructorRepositorio = instructorRepositorio;
        }
        public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
        {
            return await _instructorRepositorio.GetInstructorList();
        }
    }
  }
}