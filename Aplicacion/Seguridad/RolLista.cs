using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
  public class RolLista
  {
    public class RolListaRequest : IRequest<List<IdentityRole>>
    {}

    public class RolListaHandler : IRequestHandler<RolListaRequest, List<IdentityRole>>
    {
      private readonly CursosOnlineContext _context;

      public RolListaHandler(CursosOnlineContext context)
      {
        _context = context;
      }
      public async Task<List<IdentityRole>> Handle(RolListaRequest request, CancellationToken cancellationToken)
      {
        var roles = await _context.Roles.ToListAsync();
        return roles;
      }
    }
  }
}