﻿using RhNetAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class ClientModel
    {

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        [StringLength(14, MinimumLength =14, ErrorMessage ="O campo CNPJ deve conter 14 caracteres.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Descrição deve conter, no máximo 100 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo Situação é obrigatório.")]
        public ClientSituation Situation { get; set; }
    }
}
