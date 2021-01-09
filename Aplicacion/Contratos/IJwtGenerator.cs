using System.Collections.Generic;
using Dominio;

namespace Aplicacion.Contratos
{
  public interface IJwtGenerator
  {
    string CrearToken(User user, List<string> roles);

  }
}