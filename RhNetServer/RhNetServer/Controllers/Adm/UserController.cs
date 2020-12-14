using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using RhNetServer.Security;
using RhNetServer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public async Task<IHttpActionResult> SetClient( [FromBody] ChangeClientModel clientModel)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            pairs.Add(new KeyValuePair<string, string>("refresh_token", clientModel.Refresh_Token));
            pairs.Add(new KeyValuePair<string, string>("client_Id", "e84a2d13704647d18277966ec839d39e:CgP7NyLXtaGmyOgjj3sUMwmAlrSKqa5JyZ4P1OlfQeM"));
            pairs.Add(new KeyValuePair<string, string>("selectedClient", clientModel.ClientModel.Cnpj));

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            HttpClient client = new HttpClient();
            var tokenServiceUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/api/Security/Token";
            var response = await client.PostAsync(tokenServiceUrl, content);

            var resultContent = response.Content.ReadAsStringAsync().Result;
            
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
              
                return BadRequest(response.ReasonPhrase);
               
            }
            else
            { 
                return Ok(Newtonsoft.Json.JsonConvert.DeserializeObject(resultContent));
            }
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

        [AuthorizeAction("Visualizar Usuários")]
        [HttpGet]
        [Route("getuser")]
        public async Task<IHttpActionResult> GetUser(string userId)
        {
            UserRepository repository = new UserRepository();
            return Ok(await repository.GetUser(userManager, rhNetContext, userId));
        }

        [AuthorizeAction("Visualizar Usuários")]
        [HttpGet]
        [Route("getusers")]
        public async Task<IHttpActionResult> GetUsers()
        {
            UserRepository repository = new UserRepository();
            return Ok(await repository.GetUsers(userManager, rhNetContext, this.User.Identity.Name));
        }

        [AuthorizeAction("Adicionar Usuário")]
        [HttpPost]
        [Route("addUser")]
        public async Task<IHttpActionResult> AddUser([FromBody] ApplicationUserModel applicationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserRepository repository = new UserRepository();
            var result = await repository.AddUserAsync(userManager, rhNetContext, applicationUserModel);

            if (result == applicationUserModel)
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
        }

        [AuthorizeAction("Atualizar Usuário")]
        [HttpPost]
        [Route("updateUser")]
        public async Task<IHttpActionResult> UpdateUser([FromBody] ApplicationUserModel applicationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserRepository repository = new UserRepository();
            var result = await repository.UpdateUserAsync(userManager, rhNetContext, applicationUserModel);

            if (result == applicationUserModel)
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addRole")]
        public async Task<IHttpActionResult> AddRole( [FromBody] RoleModel role)
        {
            UserRepository repository = new UserRepository();
            return Ok(await repository.AddRoleAsync(roleManager, role));

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateRole")]
        public async Task<IHttpActionResult> UpdateRole( [FromBody] RoleModel role)
        {
            UserRepository repository = new UserRepository();
            return Ok(await repository.UpdateRoleAsync(roleManager, role));

        }
    }

}