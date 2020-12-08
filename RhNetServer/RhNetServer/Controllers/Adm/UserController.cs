using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using RhNetServer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RhNetServer.Controllers.Adm
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController: ApiController
    {
        RhNetContext rhNetContext;
        ApplicationUserManager userManager;
        ApplicationRoleManager roleManager;

        UserController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            roleManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        }

       
        [HttpPost]
        [Route("setClient")]
        public async Task<IHttpActionResult> SetClient( [FromBody] ClientModel client)
        {
            var identity = (ClaimsIdentity)this.User.Identity;

            // Change the role and create new bearer token
            identity.AddClaim(new Claim("role", "user"));

            HttpContext.Current.GetOwinContext().Authentication.SignIn((ClaimsIdentity)this.User.Identity);

            //    authenticationManager.AuthenticationResponseGrant =
            //new AuthenticationResponseGrant(
            //    new ClaimsPrincipal(identity),
            //    new AuthenticationProperties { IsPersistent = true }
            //);

            return Ok("teste");
        }

        [HttpGet]
        [Route("getAllRoles")]
        public async Task<IHttpActionResult> GetAllRoles()
        {
            UserRepository repository = new UserRepository();

            return Ok(await repository.GetAllRolesAsync(userManager, roleManager, this.User.Identity.Name));
        }

        [HttpGet]
        [Route("getroles")]
        public async Task <IHttpActionResult> GetRoles(int clientId)
        {
            UserRepository repository = new UserRepository();

            return Ok(await repository.GetRolesAsync(userManager, rhNetContext, this.User.Identity.Name, clientId));
        }
    }
}