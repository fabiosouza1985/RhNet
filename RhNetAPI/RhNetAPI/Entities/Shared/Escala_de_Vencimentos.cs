using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Shared
{
    public class Escala_de_Vencimentos
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public int Ato_Normativo_Id { get; set; }

        public Ato_Normativo Ato_Normativo { get; set; }

        
    }
}
