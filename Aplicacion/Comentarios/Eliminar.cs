using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
  public class Eliminar
  {
    public class EliminarComentarioRequest : IRequest
    {
      public Guid ComentarioId { get; set; }
    }

    public class EliminarComentarioValidacion : AbstractValidator<EliminarComentarioRequest>
    {
      public EliminarComentarioValidacion()
      {
        RuleFor(x => x.ComentarioId).NotEmpty();
      }
    }

    public class NuevoComentarioRequestHandler : IRequestHandler<EliminarComentarioRequest>
    {
      public readonly CursosOnlineContext _context;
      public NuevoComentarioRequestHandler(CursosOnlineContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(EliminarComentarioRequest request, CancellationToken cancellationToken)
      {
        var comentario = await _context.Comentario.FindAsync(request.ComentarioId) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el comentario" });;
        _context.Comentario.Remove(comentario);
        var resultados = await _context.SaveChangesAsync();
        if (resultados > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo eliminar el comentario");
      }
    }


  }
}