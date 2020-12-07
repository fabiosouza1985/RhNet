using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RhNetServer.App_Start
{
   
    public class ApplicationUserManager: UserManager<ApplicationUser, string>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> userStore) : base(userStore) { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context.Get<RhNetContext>()));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager) 
            {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = false
          };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            return manager;

        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            ///It is based on the same context as the ApplicationUserManager
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole, string, ApplicationUserRole>(context.Get<RhNetContext>()));

            return appRoleManager;
        }
    }
}