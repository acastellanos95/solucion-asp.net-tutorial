using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class EliminarCursoRequest : IRequest
        {
            public int CursoId { get; set;}
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
                var curso = await _context.Curso.FindAsync(request.CursoId) ?? throw new Exception("El curso no existe");
                _context.Curso.Remove(curso);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo actualizar el curso");
            }
        }
    }
}