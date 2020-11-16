using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class RoleModel
    {
        public string id{get; set;}
        public string Name { get; set; }
        public string Description { get; set; }

        public int? Level { get; set; }
    }
}
