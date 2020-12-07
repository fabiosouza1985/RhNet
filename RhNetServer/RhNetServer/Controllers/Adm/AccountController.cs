using Microsoft.AspNet.Identity.Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RhNetServer.Controllers.Adm
{
    [RoutePrefix("api/account")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        
        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel model)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("grant_type", "password"));
            pairs.Add(new KeyValuePair<string, string>("username", model.Username));
            pairs.Add(new KeyValuePair<string, string>("Password", model.Password));
            pairs.Add(new KeyValuePair<string, string>("teste", "123456"));
            pairs.Add(new KeyValuePair<string, string>("client_Id", "e84a2d13704647d18277966ec839d39e:CgP7NyLXtaGmyOgjj3sUMwmAlrSKqa5JyZ4P1OlfQeM"));

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            HttpClient client = new HttpClient();
            var tokenServiceUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/api/Security/Token";
            var response = await client.PostAsync(tokenServiceUrl, content);

            string resultContent = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                var erros = Newtonsoft.Json.JsonConvert.DeserializeObject< Dictionary< string, string>>(resultContent);
                for(var i = 0; i < erros.Count; i++)
                {
                   
                    ModelState.AddModelError(erros.ElementAt(i).Key, erros.ElementAt(i).Value);
                }
                return BadRequest(ModelState);
            }
            else
            {
                UserRepository repository = new UserRepository();
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByNameAsync(model.Username);

                var rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();

                var clients = await repository.GetClientsAsync(rhNetContext, user.Id);

                if (clients.Count() == 0)
                {
                    if (user.UserName == "master")
                    {
                        ClientRepository clientRepository = new ClientRepository();
                        clients = await clientRepository.GetAllClients(rhNetContext);
                    }
                    else
                    {
                        return BadRequest("Cliente não associado a um cliente");
                    }
                }

                //if (clients.Count() > 1 && model.SelectedClient == null)
                //{       
                //    return Content(HttpStatusCode.Conflict, clients);
                //}

                //ClientModel selectedClient = null;


                //if (clients.Count() == 1)
                //{
                //    selectedClient = clients[0];
                //}
                //else
                //{
                //    for (var i = 0; i < clients.Count; i++)
                //    {
                //        if (model.SelectedClient.Cnpj == clients[i].Cnpj)
                //        {
                //            selectedClient = clients[i];
                //            break;
                //        }
                //    }
                //}

                //if (selectedClient == null)
                //{
                //    return BadRequest("Cliente não associado a um cliente ou cliente selecionado incorreto");
                //}

                //if (selectedClient.Situation == Enums.ClientSituation.Bloqueado && user.UserName != "master")
                //{
                //    return BadRequest("Cliente bloqueado. Entre em contato com um administrador do sistema");
                //}

                //if (selectedClient.Situation == Enums.ClientSituation.Inativo && user.UserName != "master")
                //{
                //    return BadRequest("Cliente inativo. Entre em contato com um administrador do sistema");
                //}


                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(resultContent);

                

                //result.currentClient = Newtonsoft.Json.JsonConvert.SerializeObject(new 
                //{
                //    id = selectedClient.Id, 
                //    cnpj = selectedClient.Cnpj ,
                //    description = selectedClient.Description,
                //    situation = selectedClient.Situation
                //});
               
                return Ok(result);
            }
        }


    }
}