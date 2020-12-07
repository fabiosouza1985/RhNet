using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Shared
{
    [Table("EntidadesSubordinacoes")]
    public class EntidadeSubordinacao
    {
       
        [Column("Entidade_Superior_Id", Order =0)]
        [ForeignKey("Entidade_Superior")]
        public int Entidade_Superior_Id { get; set; }

        [Column("Entidade_Superior")]
        [ForeignKey("Entidade_Superior_Id")]
        public Entidade Entidade_Superior { get; set; }

      
        [Column("Entidade_Inferior_Id", Order = 1)]
        [ForeignKey("Entidade_Inferior")]
        public int Entidade_Inferior_Id { get; set; }

        [Column("Entidade_Inferior")]
        [ForeignKey("Entidade_Inferior_Id")]
        public Entidade Entidade_Inferior { get; set; }

       
        [Column("Vigencia_Inicio", Order = 2)]
        public DateTime Vigencia_Inicio { get; set; }

        [Column("Vigencia_Termino")]
        public DateTime? Vigencia_Termino { get; set; }
    }
}
