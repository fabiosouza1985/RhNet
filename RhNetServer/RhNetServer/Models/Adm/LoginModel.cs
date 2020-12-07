using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Models.Adm
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ClientModel SelectedClient { get; set; }
    }
}
