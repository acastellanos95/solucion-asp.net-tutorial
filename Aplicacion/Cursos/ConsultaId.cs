using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }
        }

    public class CursoUnicoHandler : IRequestHandler<CursoUnico, Curso>
    {
        private readonly CursosOnlineContext _context;
        public CursoUnicoHandler(CursosOnlineContext context)
        {
            _context = context;
        }

        public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
        {
            var curso = await _context.Curso.FindAsync(request.Id) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el curso"});
            return curso;
        }
    }
  }
}