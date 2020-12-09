using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RhNetServer.Models.Adm
{
    public class ChangeClientModel
    {
        public ClientModel ClientModel { get; set; }
        public string Refresh_Token { get; set; }
    }
}