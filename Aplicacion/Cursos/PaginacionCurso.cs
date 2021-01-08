using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Paginacion;

namespace Aplicacion.Cursos
{
  public class PaginacionCurso
  {
    public class PaginacionCursoRequest : IRequest<PaginacionModel>
    {
      public string Titulo { get; set; }
      public int NumeroPagina { get; set; }
      public int CantidadElementos { get; set; }
    }

    public class PaginacionCursoHandler : IRequestHandler<PaginacionCursoRequest, PaginacionModel>
    {
      private readonly IPaginacion _paginacion;
      public PaginacionCursoHandler(IPaginacion paginacion)
      {
        _paginacion = paginacion;
      }
      public async Task<PaginacionModel> Handle(PaginacionCursoRequest request, CancellationToken cancellationToken)
      {
        var storedProcedure = "usp_Obtener_Paginacion";
        var ordenamiento = "Titulo";
        var parametros = new Dictionary<string, object>();
        parametros.Add("NombreCurso", request.Titulo);
        return await _paginacion.DevolverPaginacion(storedProcedure, request.NumeroPagina, request.CantidadElementos, parametros, ordenamiento);
      }
    }
  }
}