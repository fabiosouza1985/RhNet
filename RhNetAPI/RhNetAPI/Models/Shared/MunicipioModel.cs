using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Shared
{
    public class MunicipioModel
    {
        [Required(ErrorMessage ="Campo {0} obrigatório")]
        [Display(AutoGenerateField =false, Name ="Id", Order =0, Description ="Id", AutoGenerateFilter =false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(300, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Descricao", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Descricao { get; set; }

        [StringLength(10, ErrorMessage = "O Campo {0} deve conter no máximo {1} Caracter(es)!")]
        [Display(AutoGenerateField = true, Name = "Codigo_Audesp", Order = 2, Description = "Código Audesp", AutoGenerateFilter = true)]
        public string Codigo_Audesp { get; set; }
    }
}
