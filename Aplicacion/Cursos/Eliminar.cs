using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class EliminarCursoRequest : IRequest
        {
            public Guid CursoId { get; set;}
        }

        public class EliminarCursoRequestHandler : IRequestHandler<EliminarCursoRequest>
        {
            public readonly CursosOnlineContext _context;
            public EliminarCursoRequestHandler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EliminarCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });

                // Remover instructores de la tabla cursoinstructor
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId);
                foreach (var instructor in instructoresDB)
                {
                    _context.CursoInstructor.Remove(instructor);
                }
                
                // Remover precio
                var precioDB = _context.Precio.Where(x => x.CursoId == request.CursoId).FirstOrDefault();
                _context.Precio.Remove(precioDB);

                // Remover comentarios
                var comentariosDB = _context.Comentario.Where(x => x.CursoId == request.CursoId);
                foreach (var comentario in comentariosDB)
                {
                    _context.Comentario.Remove(comentario);
                }

                _context.Curso.Remove(curso);

                var valor = await _context.SaveChangesAsync();
                
                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}