using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Shared
{
    public class Classe
    {

        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Escala_de_Vencimentos { get; set; }

        public string Estrutura { get; set; }
        public string Subquadro_de_Cargo { get; set; }

        public string Subquadro_de_Funcao { get; set; }

        public int? Referencia_Inicial { get; set; }

        public int? Referencia_Final { get; set; }

    }
}
