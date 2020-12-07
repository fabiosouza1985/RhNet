using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RhNetServer.Entities.Shared
{
    [Table("Tipos_de_Ato_Normativo")]
    public class Tipo_de_Ato_Normativo
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
