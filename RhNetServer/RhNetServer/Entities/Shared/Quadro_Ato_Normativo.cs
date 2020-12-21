using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RhNetServer.Entities.Shared
{
    [Table("Quadros_Atos_Normativos")]
    public class Quadro_Ato_Normativo
    {

        [Key]
        [Required]
        [Column("Quadro_Id", Order =0)]
        [ForeignKey("Quadro")]
        public int Quadro_Id { get; set; }

        [Column("Quadro")]
        [ForeignKey("Quadro_Id")]
        public Quadro Quadro { get; set; }

        [Key]
        [Required]
        [Column("Ato_Normativo_Id", Order = 1)]
        [ForeignKey("Ato_Normativo")]
        public int Ato_Normativo_Id { get; set; }

        [Column("Ato_Normativo")]
        [ForeignKey("Ato_Normativo_Id")]
        public Ato_Normativo Ato_Normativo { get; set; }
    }
}