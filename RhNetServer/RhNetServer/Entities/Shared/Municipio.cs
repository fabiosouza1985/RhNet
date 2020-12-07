using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Shared
{
    [Table("Municipios")]
    public class Municipio
    {

        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Descricao")]
        [Required]
        [StringLength(300)]
        public string Descricao { get; set; }


        [Column("Codigo_Audesp")]
        [StringLength(10)]
        public string Codigo_Audesp { get; set; }
    }
}
