using RhNetAPI.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Models.Adm
{
    public class ApplicationUserModel
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public List<Client> clients { get; set; }
        public List<ApplicationRole> applicationRoles { get; set; }
        public List<Permission> permissions { get; set; }
    }
}
