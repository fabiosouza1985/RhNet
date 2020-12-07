using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Models.Adm
{
    public class PermissionModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [Display(AutoGenerateField = false, Name = "Id", Order = 0, Description = "Id", AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Description", Order = 1, Description = "Descrição", AutoGenerateFilter = true)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} é Obrigatório e deve conter no máximo {1} Caracteres!")]
        [Display(AutoGenerateField = true, Name = "Table", Order = 2, Description = "Tabela", AutoGenerateFilter = true)]
        public string Table { get; set; }

    }
}
