using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
  public class Nuevo
  {
    public class NuevoComentarioRequest : IRequest
    {
      public string Alumno { get; set; }
      public int Puntaje { get; set; }
      public string ComentarioTexto { get; set; }
      public Guid CursoId { get; set; }
    }

    public class NuevoComentarioValidacion : AbstractValidator<NuevoComentarioRequest>
    {
      public NuevoComentarioValidacion()
      {
        RuleFor(x => x.Alumno).NotEmpty();
        RuleFor(x => x.Puntaje).NotEmpty();
        RuleFor(x => x.ComentarioTexto).NotEmpty();
        RuleFor(x => x.CursoId).NotEmpty();
      }
    }

    public class NuevoComentarioRequestHandler : IRequestHandler<NuevoComentarioRequest>
    {
      public readonly CursosOnlineContext _context;
      public NuevoComentarioRequestHandler(CursosOnlineContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(NuevoComentarioRequest request, CancellationToken cancellationToken)
      {
        Guid _comentarioId = Guid.NewGuid();
        var comentario = new Comentario
        {
          ComentarioId = _comentarioId,
          Alumno = request.Alumno,
          Puntaje = request.Puntaje,
          ComentarioTexto = request.ComentarioTexto,
          CursoId = request.CursoId,
          FechaCreacion = DateTime.UtcNow
        };
        _context.Comentario.Add(comentario);

        var resultados = await _context.SaveChangesAsync();
        if (resultados > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo insertar el comentario");
      }
    }

  }
}