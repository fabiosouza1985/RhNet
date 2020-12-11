using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RhNetServer.Models.Shared
{
    public class EntidadeModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Id", Order = 0, Description = "Id", AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(300, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Descricao", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Descricao { get; set; }

        [StringLength(10, ErrorMessage = "O Campo {0} deve conter no máximo {1} Caracter(es)!")]
        [Display(AutoGenerateField = true, Name = "Codigo_Audesp", Order = 2, Description = "Código Audesp", AutoGenerateFilter = true)]
        public string Codigo_Audesp { get; set; }

        
        [Display(AutoGenerateField = false, Name = "Municipio_Id", Order = 2, Description = "Id do Município", AutoGenerateFilter = false)]
        public int? Municipio_Id { get; set; }
        
        [ReadOnly(true)]
        [Display(AutoGenerateField = false, Name = "Municipio_Descricao", Order = 2, Description = "Município", AutoGenerateFilter = false)]
        public string Municipio_Descricao { get; set; }       

        [Display(AutoGenerateField = false, Name = "Entidades_Subordinadas", Order = 2, Description = "Entidades Subordinadas", AutoGenerateFilter = false)]
        public List<EntidadeModel> Entidades_Subordinadas { get; set; }

    }
}
