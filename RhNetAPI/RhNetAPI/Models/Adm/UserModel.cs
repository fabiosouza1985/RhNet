using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<RoleModel> Profiles { get; set; }
        public ClientModel CurrentClient { get; set; }
    }
}
