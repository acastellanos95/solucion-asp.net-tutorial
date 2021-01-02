using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class LoginRequest : IRequest<UserData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginValidator : AbstractValidator<LoginRequest>
        {
            public LoginValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();    
            }
        }

        public class LoginHandler : IRequestHandler<LoginRequest, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;

            public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<UserData> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new ExceptionHandling(HttpStatusCode.Unauthorized, new { message = "No se encontró el mail"});
                }
                var res = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if(res.Succeeded)
                {
                    return new UserData
                    {
                        NombreCompleto = user.NombreCompleto,
                        Token = "Falta implementación",
                        Username = user.UserName,
                        Email = user.Email,
                        Imagen = null,
                    };
                }
                throw new ExceptionHandling(HttpStatusCode.Unauthorized);
            }
        }
    }
}