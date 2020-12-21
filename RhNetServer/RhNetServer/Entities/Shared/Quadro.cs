using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Shared
{
    [Table("Quadros")]
    public class Quadro
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Descricao")]
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
    }
}
