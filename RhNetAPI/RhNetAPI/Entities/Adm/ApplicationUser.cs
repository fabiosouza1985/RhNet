using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Adm
{
    public class ApplicationUser : IdentityUser
    {

        [Column("Cpf", TypeName ="Char(11)")]
        public string Cpf { get; set; }
    }
}
