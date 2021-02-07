using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager){
            if(!usuarioManager.Users.Any()){
                var user = new Usuario
                {
                    NombreCompleto = "Andre Castellanos",
                    UserName = "andre95",
                    Email = "ak47andre95@gmail.com"
                };
                await usuarioManager.CreateAsync(user, "Yoongihoseok95$");
            }
        }
    }
}