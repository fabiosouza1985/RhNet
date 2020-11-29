using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RhNetAPI.Entities.Shared
{
    [Table("Atos_Normativos")]
    public class Ato_Normativo
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Descricao")]
        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Column("Numero")]
        [Required]
        public int Numero { get; set; }

        [Column("Ano")]
        [Required]
        [Range(1900,2100)]
        public int Ano { get; set; }

        [Column("Vigencia_Data")]
        [Required]
        public DateTime Vigencia_Data { get; set; }

        [Column("Publicacao_Data")]
        public DateTime? Publicacao_Data { get; set; }

        [Required]
        [Column("Tipo_de_Ato_Normativo_Id")]
        [ForeignKey("Tipo_de_Ato_Normativo")]
        public int Tipo_de_Ato_Normativo_Id { get; set; }

        [Column("Tipo_de_Ato_Normativo")]
        [ForeignKey("Tipo_de_Ato_Normativo_Id")]
        public Tipo_de_Ato_Normativo Tipo_de_Ato_Normativo { get; set; }
    }
}
