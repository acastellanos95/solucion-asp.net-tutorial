using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
  public class Eliminar
  {
    public class EliminarInstructorRequest : IRequest
    {
      public Guid InstructorId { get; set; }
    }

    public class EliminarInstructorValidacion : AbstractValidator<EliminarInstructorRequest>
    {
      public EliminarInstructorValidacion()
      {
        RuleFor(x => x.InstructorId).NotEmpty();
      }
    }

    public class EliminarInstructorHandler : IRequestHandler<EliminarInstructorRequest>
    {
      private readonly IInstructor _instructor;
      public EliminarInstructorHandler(IInstructor instructor)
      {
        _instructor = instructor;
      }

      public async Task<Unit> Handle(EliminarInstructorRequest request, CancellationToken cancellationToken)
      {

        var result = await _instructor.Delete(request.InstructorId);
        if (result > 0) return Unit.Value;
        throw new Exception("No se pudo insertar el instructor");
      }
    }
  }
}