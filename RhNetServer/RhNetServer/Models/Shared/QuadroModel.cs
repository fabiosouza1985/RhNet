using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RhNetServer.Models.Shared
{
    public class QuadroModel
    {

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Id", Order = 0, Description = "Id", AutoGenerateFilter = false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(300, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Descricao", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Descricao { get; set; }

        [Display(AutoGenerateField = false, Name = "Subquadros", Order = 2, Description = "Subquadros", AutoGenerateFilter = false)]
        public List<SubquadroModel> Subquadros { get; set; }

        [Display(AutoGenerateField = false, Name = "Atos_Normativos", Order = 3, Description = "Atos Normativos", AutoGenerateFilter = false)]
        public List<Ato_NormativoModel> Atos_Normativos { get; set; }

        [ReadOnly(true)]
        [Display(AutoGenerateField = true, Name = "Total_Subquadros", Order = 4, Description = "Subquadros", AutoGenerateFilter = true)]
        public int Total_Subquadros
        {
            get {
                if (Subquadros == null) return 0;
                return  Subquadros.Count(); 
            }

        }

        [ReadOnly(true)]
        [Display(AutoGenerateField = true, Name = "Total_Atos_Normativos", Order = 5, Description = "Atos Normativos", AutoGenerateFilter = true)]
        public int Total_Atos_Normativos
        {
            get {
                if (Atos_Normativos == null) return 0;
                return Atos_Normativos.Count(); 
            }
        }
    }
}
