using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad
{
  public class JwtGenerator : IJwtGenerator
  {
    public string CrearToken(User user)
    {
      var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
      var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescription = new SecurityTokenDescriptor 
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(30),
        SigningCredentials = credenciales
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescription);

      return tokenHandler.WriteToken(token);
    }
  }
}