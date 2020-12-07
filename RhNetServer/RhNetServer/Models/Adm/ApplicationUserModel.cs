using RhNetServer.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Models.Adm
{
    public class ApplicationUserModel
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public List<ClientModel> clients { get; set; }
        public List<UserRoleModel> applicationRoles { get; set; }
        public List<UserPermissionModel> permissions { get; set; }

        public class UserRoleModel
        {
            public string UserId { get; set; }
            public int ClientId { get; set; }
            public string RoleId { get; set; }
        }

        public class UserPermissionModel
        {
            public string UserId { get; set; }
            public int ClientId { get; set; }

            public string Description { get; set; }

        }
    }

    
}
