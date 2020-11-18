using RhNetAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Adm
{
    [Table("Clients")]
    public class Client
    {

        [Key()]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Cnpj", TypeName ="CHAR(14)")]
        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [Column("Description")]
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Column("Situation")]
        [Required]
        public ClientSituation Situation { get; set; }
    }
}
