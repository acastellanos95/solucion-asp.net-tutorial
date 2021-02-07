using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class NuevoCursoRequest : IRequest
        {
            public Guid? CursoId {get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public List<Guid> ListInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }
        }

        public class NuevoCursoValidacion : AbstractValidator<NuevoCursoRequest>
        {
            public NuevoCursoValidacion()
            {
                RuleFor( x => x.Titulo).NotEmpty();
                RuleFor( x => x.Descripcion).NotEmpty();
                RuleFor( x => x.FechaPublicacion).NotEmpty();
            }
        }

        public class NuevoCursoRequestHandler : IRequestHandler<NuevoCursoRequest>
        {
            public readonly CursosOnlineContext _context;
            public NuevoCursoRequestHandler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(NuevoCursoRequest request, CancellationToken cancellationToken)
            {
                Guid _cursoId = Guid.NewGuid();
                if(request.CursoId != null)
                {
                    _cursoId = request.CursoId ?? Guid.NewGuid() ;
                }
                var curso = new Curso 
                { 
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                };
                _context.Curso.Add(curso);

                if (request.ListInstructor != null)
                {
                    foreach (var id in request.ListInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = _cursoId,
                            InstructorId = id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                var precioEntidad = new Precio
                {
                    CursoId = _cursoId,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    PrecioId = Guid.NewGuid()
                };
                _context.Precio.Add(precioEntidad);

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}