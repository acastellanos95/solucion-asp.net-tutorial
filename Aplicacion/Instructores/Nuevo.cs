using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
    public class Nuevo
    {
        public class NuevoInstructorRequest : IRequest
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }
        }

        public class NuevoInstructorRequestValidator : AbstractValidator<NuevoInstructorRequest>
        {
            public NuevoInstructorRequestValidator()
            {
                RuleFor( x => x.Nombre).NotEmpty();
                RuleFor( x => x.Apellidos).NotEmpty();
                RuleFor( x => x.Grado).NotEmpty();
            }
        }
        public class NuevoInstructorHandler : IRequestHandler<NuevoInstructorRequest>
        {
            private readonly IInstructor _instructor;
            public NuevoInstructorHandler(IInstructor instructor)
            {
                _instructor = instructor;
            }

            public async Task<Unit> Handle(NuevoInstructorRequest request, CancellationToken cancellationToken)
            {
                var result = await _instructor.New(request.Nombre, request.Apellidos, request.Grado);
                if(result > 0) return Unit.Value;
                throw new Exception("No se pudo insertar el instructor");
            }
        }
    }
}