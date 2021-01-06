using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
  public class Editar
  {
    public class EditarInstructorRequest : IRequest
    {
      public Guid InstructorId { get; set; }
      public string Nombre { get; set; }
      public string Apellidos { get; set; }
      public string Grado { get; set; }
    }

    public class EditarInstructorValidacion : AbstractValidator<EditarInstructorRequest>
    {
      public EditarInstructorValidacion()
      {
        RuleFor(x => x.InstructorId).NotEmpty();
        RuleFor(x => x.Nombre).NotEmpty();
        RuleFor(x => x.Apellidos).NotEmpty();
        RuleFor(x => x.Grado).NotEmpty();
      }
    }

    public class EditarInstructorHandler : IRequestHandler<EditarInstructorRequest>
    {
      private readonly IInstructor _instructor;
      public EditarInstructorHandler(IInstructor instructor)
      {
        _instructor = instructor;
      }

      public async Task<Unit> Handle(EditarInstructorRequest request, CancellationToken cancellationToken)
      {

        var result = await _instructor.Update(request.InstructorId, request.Nombre, request.Apellidos, request.Grado);
        if (result > 0) return Unit.Value;
        throw new Exception("No se pudo insertar el instructor");
      }
    }
  }
}