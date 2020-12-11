using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RhNetServer.Models.Shared
{
    public class Ato_NormativoModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Id", Order = 0, Description = "Id", AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(500, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Descricao", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = true, Name = "Numero", Order = 2, Description = "Número", AutoGenerateFilter = true)]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} deve ser um número igual ou maior a {1}")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = true, Name = "Ano", Order = 3, Description = "Ano", AutoGenerateFilter = true)]
        [Range(1900, 2100, ErrorMessage = "O campo {0} deve ser um número entre {1} e {2}")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = true, Name = "Vigencia_Data", Order = 4, Description = "Vigência", AutoGenerateFilter = true)]
        [Range(typeof(DateTime), "01/01/1900", "31/12/2500", ErrorMessage = "O campo {0} deve ser um número igual ou maior a {1}")]
        public DateTime Vigencia_Data { get; set; }

        [Display(AutoGenerateField = true, Name = "Publicacao_Data", Order = 5, Description = "Publicação", AutoGenerateFilter = true)]
        public Nullable< DateTime> Publicacao_Data { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Tipo_de_Ato_Normativo_Id", Order = 6, Description = "Tipo de Ato Normativo - Id", AutoGenerateFilter = false)]
        public int Tipo_de_Ato_Normativo_Id { get; set; }

        [Display(AutoGenerateField = false, Name = "Tipo_de_Ato_Normativo_Descricao", Order = 7, Description = "Tipo de Ato Normativo - Descrição", AutoGenerateFilter = false)]
        public string Tipo_de_Ato_Normativo_Descricao { get; set; }
    }
}
