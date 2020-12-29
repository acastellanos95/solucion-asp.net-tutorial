using System;
using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public class User : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}