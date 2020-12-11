using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RhNetServer.Models.Shared
{
    public class Tipo_de_Ato_NormativoModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Id", Order = 0, Description = "Id", AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Descricao", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Descricao { get; set; }
    }
}
