using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertData(CursosOnlineContext context, UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new User 
                {
                    NombreCompleto = "Andre Castellanos",
                    UserName = "andre95",
                    Email = "ak47andre95@gmail.com"
                };
                await userManager.CreateAsync(user, "Yoongihoseok95$");
            }
        }
    }
}