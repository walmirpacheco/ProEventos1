using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos1.Domain.Enum;

namespace ProEventos1.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; } 
        public Funcao Funcao { get; set; }
        public string ImagemURL { get; set; } 
        public IEnumerable<UserRole> UserRoles { get; set; }            
    }
}