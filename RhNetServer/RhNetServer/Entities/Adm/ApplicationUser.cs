using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Adm
{
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {

        [Column("Cpf", TypeName ="Char")]
        [StringLength(11)]
        public string Cpf { get; set; }

        [Column("LockoutEnd")]
        public override DateTime? LockoutEndDateUtc { get => base.LockoutEndDateUtc; set => base.LockoutEndDateUtc = value; }
    }
}
