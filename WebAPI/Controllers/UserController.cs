using System;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        //http://localhost:5000/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> UserLogin([FromBody]Login.LoginRequest parametros)
        {
            return await Mediator.Send(parametros);
        }

        //http://localhost:5000/api/User/register
        [HttpPost("register")]
        public async Task<ActionResult<UserData>> UserRegister([FromBody] Register.RegisterRequest parametros)
        {
            return await Mediator.Send(parametros);
        }

        //http://localhost:5000/api/User
        [HttpGet]
        public async Task<ActionResult<UserData>> ReturnUser()
        {
            return await Mediator.Send(new CurrentUser.CurrentUserRequest());
        }
    }
}