using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Adm
{
    [Table("Permissions")]
    public class Permission
    {
        [Key()]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Description")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Column("Table")]
        [StringLength(100)]
        public string Table { get; set; }

    }
}
