using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Table { get; set; }
    }
}
