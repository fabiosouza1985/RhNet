using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RhNetAPI.Entities.Adm
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        [Column("ClientId")]       
        [ForeignKey("Client")]
        public int? ClientId { get; set; }

        [Column("Client")]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
