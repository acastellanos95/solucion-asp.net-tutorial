using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Paginacion
{
  public interface IPaginacion
  {
    Task<PaginacionModel> DevolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna);
  }
}