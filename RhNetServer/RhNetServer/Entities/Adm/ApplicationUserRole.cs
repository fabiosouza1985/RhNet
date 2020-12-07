using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RhNetServer.Entities.Adm
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        [Column("ClientId")]
        [ForeignKey("Client")]
        public int? ClientId { get; set; }

        [Column("Client")]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
