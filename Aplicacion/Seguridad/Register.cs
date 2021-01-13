using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ErrorHandling;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
  public class Register
  {
    public class RegisterRequest : IRequest<UserData>
    {
      public string Username { get; set; }
      public string NombreCompleto { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
    }

    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
      public RegisterValidator()
      {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.NombreCompleto).NotEmpty();
      }
    }

    public class RegisterHandler : IRequestHandler<RegisterRequest, UserData>
    {
      private readonly CursosOnlineContext _context;
      private readonly UserManager<User> _user;
      private readonly IJwtGenerator _jwtGenerator;
      public RegisterHandler(CursosOnlineContext context, UserManager<User> user, IJwtGenerator jwtGenerator)
      {
        _context = context;
        _user = user;
        _jwtGenerator = jwtGenerator;
      }
      public async Task<UserData> Handle(RegisterRequest request, CancellationToken cancellationToken)
      {
        var existEmail = await _context.Users.Where( x => x.Email == request.Email).AnyAsync();
        if (existEmail)
        { 
          throw new ExceptionHandling(HttpStatusCode.BadRequest, new { mensaje = "El email ingresado ya existe"});
        }

        var existUsername = await _context.Users.Where( x => x.UserName == request.Username).AnyAsync();
        if (existUsername)
        { 
          throw new ExceptionHandling(HttpStatusCode.BadRequest, new { mensaje = "El Username ingresado ya existe"});
        }

        var user = new User
        {
          NombreCompleto = request.NombreCompleto,
          Email = request.Email,
          UserName = request.Username
        };

        var result = await _user.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
          return new UserData
          {
            NombreCompleto = user.NombreCompleto,
            Token = _jwtGenerator.CrearToken(user, null),
            Username = user.UserName,
            Email = user.Email
          };
        }

        throw new Exception("No se pudo agregar al usuario");
      }
    }
  }
}