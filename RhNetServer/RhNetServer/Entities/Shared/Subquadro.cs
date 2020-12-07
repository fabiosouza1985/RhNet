using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Shared
{
    [Table("Subquadros")]
    public class Subquadro
    {

        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Descricao")]
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Column("Sigla")]
        [Required]
        [StringLength(10)]
        public string Sigla { get; set; }

        [Required]
        [Column("Quadro_Id")]
        [ForeignKey("Quadro")]
        public int Quadro_Id { get; set; }

        [Column("Quadro")]
        [ForeignKey("Quadro_Id")]
        public Quadro Quadro { get; set; }
    }
}
