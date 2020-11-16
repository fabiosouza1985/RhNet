using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Adm
{
    [Table("Favorites")]
    public class Favorite
    {
        [Key()]
        [Column("Id")]
        public int Id { get; set; }

       
        [Column("UserId", Order =0)]
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

       
        [Column("MenuId", Order = 1)]
        [Required]
        [ForeignKey("ApplicationMenu")]
        public int MenuId { get; set; }

        [Column("ApplicationUser")]
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Column("ApplicationMenu")]
        [ForeignKey("MenuId")]
        public ApplicationMenu ApplicationMenu { get; set; }
    }
}
