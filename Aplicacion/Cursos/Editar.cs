using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using AutoMapper;
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
            public Guid CursoId { get; set;}
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> InstructorList { get; set; }
            public decimal? Precio { get; set; }
            public decimal? Promocion { get; set; }
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
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public EditarCursoRequestHandler(CursosOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(EditarCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el curso"});

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                curso.FechaCreacion = DateTime.UtcNow;

                // Actualizar precio de cursos

                var precioEntidad = _context.Precio.Where(x => x.CursoId == request.CursoId).FirstOrDefault();
                if(precioEntidad != null)
                {
                    precioEntidad.Promocion = request.Promocion ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                }
                else
                {
                    precioEntidad = new Precio
                    {
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.Promocion ?? 0,
                        CursoId = curso.CursoId
                    };
                    await _context.Precio.AddAsync(precioEntidad);
                }

                if (request.InstructorList != null)
                {
                    if(request.InstructorList.Count > 0)
                    {
                        // Eliminar los instructores actuales del curso en la base de datos
                        var instructoresBD = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId).ToList();
                        foreach (var id in instructoresBD)
                        {
                            _context.CursoInstructor.Remove(id);
                        }

                        // Agregar instructores que provengan del cliente
                        foreach (var id in request.InstructorList)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = id
                            };
                            _context.CursoInstructor.Add(nuevoInstructor);
                        }
                    }
                }

                
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