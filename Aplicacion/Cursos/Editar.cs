using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class EditarCursoRequest : IRequest
        {
            public int CursoId { get; set;}
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class EditarCursoRequestHandler : IRequestHandler<EditarCursoRequest>
        {
            public readonly CursosOnlineContext _context;
            public EditarCursoRequestHandler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EditarCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId) ?? throw new Exception("El curso no existe");

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                
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