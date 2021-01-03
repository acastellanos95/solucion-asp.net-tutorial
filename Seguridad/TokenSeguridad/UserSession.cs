using System.Linq;
using System.Security.Claims;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;

namespace Seguridad
{
  public class UserSession : IUserSession
  {
    private readonly IHttpContextAccessor _httpContextAcsessor;
    public UserSession(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAcsessor = httpContextAccessor;
    }
    public string ObtainUserSession()
    {
      var username = _httpContextAcsessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
      return username;
    }
  }
}