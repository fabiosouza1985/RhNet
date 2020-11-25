using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Adm
{
    public class ApplicationRole : IdentityRole
    {

        [Column("Description")]
        [StringLength(200)]
        public string Description { get; set; }

        [Column("Level")]
        public int? Level { get; set; }
    }
}
