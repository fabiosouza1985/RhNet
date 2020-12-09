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

            string resultContent = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var erros = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(resultContent);
                
                for (var i = 0; i < erros.Count; i++)
                {
                    ModelState.AddModelError(erros.ElementAt(i).Key, erros.ElementAt(i).Value);

                }
                return BadRequest(ModelState);
            }
            else
            {
               
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(resultContent);

                result.currentClient = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    id = clientModel.ClientModel.Id,
                    cnpj = clientModel.ClientModel.Cnpj,
                    description = clientModel.ClientModel.Description,
                    situation = clientModel.ClientModel.Situation
                });
                
                return Ok(result);
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
    }
}