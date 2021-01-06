using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
  public class ConsultaId
  {
    public class ConsultaIdInstructorRequest : IRequest<InstructorModel>
    {
      public Guid InstructorId { get; set; }
    }

    public class ConsultaIdInstructorValidacion : AbstractValidator<ConsultaIdInstructorRequest>
    {
      public ConsultaIdInstructorValidacion()
      {
        RuleFor(x => x.InstructorId).NotEmpty();
      }
    }

    public class ConsultaIdInstructorHandler : IRequestHandler<ConsultaIdInstructorRequest, InstructorModel>
    {
      private readonly IInstructor _instructor;
      public ConsultaIdInstructorHandler(IInstructor instructor)
      {
        _instructor = instructor;
      }

      public async Task<InstructorModel> Handle(ConsultaIdInstructorRequest request, CancellationToken cancellationToken)
      {

        var result = await _instructor.GetById(request.InstructorId);
        if(result == null) throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el instructor"})
        return result;
      }
    }
  }
}