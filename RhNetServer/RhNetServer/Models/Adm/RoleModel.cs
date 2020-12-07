using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Models.Adm
{
    public class RoleModel
    {
        public string Id{get; set;}
        public string Name { get; set; }
        public string Description { get; set; }

        public int? Level { get; set; }
    }
}
