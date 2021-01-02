using System;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        //http://localhost:5000/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> UserLogin([FromBody]Login.LoginRequest parametros)
        {
            return await Mediator.Send(parametros);
        }
    }
}