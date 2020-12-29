using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using Dominio;
using FluentValidation;
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

        public class EditarCursosValidacion : AbstractValidator<EditarCursoRequest>
        {
            public EditarCursosValidacion()
            {
                RuleFor( x => x.Titulo).NotEmpty();
                RuleFor( x => x.Descripcion).NotEmpty();
                RuleFor( x => x.FechaPublicacion).NotEmpty();
            }
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
                var curso = await _context.Curso.FindAsync(request.CursoId) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el curso"});

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