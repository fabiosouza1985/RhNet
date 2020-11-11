using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class MenuModel
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Header { get; set; }

        [Required]
        [StringLength(100)]
        public string Path { get; set; }

        [StringLength(256)]
        [Required]
        public string Role_Name { get; set; }
                
        [Required]
        [StringLength(100)]
        public string Permission_Name { get; set; }
    }
}
