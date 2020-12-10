using Microsoft.AspNet.Identity.Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create()
        {
            try
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
                var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();

                var userMaster = await userManager.FindByNameAsync("master");
                var userFabio = await userManager.FindByNameAsync("fabio");

                if(userMaster == null)
                {
                   await userManager.CreateUser(new ApplicationUser() {Id= "18f6b2ef-1dae-4d54-a56b-82a396b0ae6f", Email = "master@email.com", UserName = "master", Cpf = "11111111111" }, "Mm123456*");
                    
                    userMaster = await userManager.FindByNameAsync("master");
                }

                if (userFabio == null)
                {
                  
                    await userManager.CreateUser(new ApplicationUser() { Id = "8afa24f2-02ed-4379-8157-c9812fe624a2", Email = "fabio@email.com", UserName = "fabio", Cpf = "22222222222"}, "Mm123456*");
                    userFabio = await userManager.FindByNameAsync("fabio");
                }

                if (rhnetContext.Clients.Count() == 0)
                {
                    rhnetContext.Clients.Add(new Client() { Cnpj = "11111111111111", Description = "Empresa 01", Situation = Enums.ClientSituation.Ativo });
                    rhnetContext.Clients.Add(new Client() { Cnpj = "22222222222222", Description = "Empresa 02", Situation = Enums.ClientSituation.Bloqueado });
                    rhnetContext.Clients.Add(new Client() { Cnpj = "33333333333333", Description = "Empresa 03", Situation = Enums.ClientSituation.Ativo });
                    rhnetContext.Clients.Add(new Client() { Cnpj = "44444444444444", Description = "Empresa 04", Situation = Enums.ClientSituation.Ativo });

                    await rhnetContext.SaveChangesAsync();
                }

                if (roleManager.Roles.Count() == 0)
                {
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Master", Description = "Acesso Total ao sistema. Pode cadastrar novos usuários, clientes e empresas." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Administrador", Description = "Acesso e cadastro das tabelas em comum do sistema. Pode cadastrar Super Usuários." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Super Usuário", Description = "Cadastra Usuário do RH." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "RH", Description = "Cadastra funcionários, atualiza dados dos funcionários e tabelas específicas." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Gestor Mediato", Description = "Responsável por autorizar as solicitações do Superior Mediato." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Gestor Imediato", Description = "Responsável por autorizar as solicitações de seus suborinados, lançamento de férias, entre outros." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Funcionário", Description = "Consulta informações próprias no sistema, entre outros." });

                    var role_1 =await roleManager.FindByNameAsync("Master");
                    var role_2 = await roleManager.FindByNameAsync("Administrador");
                    
                    var client_1 = await (from x in rhnetContext.Clients where x.Cnpj == "11111111111111" select x).FirstOrDefaultAsync();
                    var client_2 = await (from x in rhnetContext.Clients where x.Cnpj == "22222222222222" select x).FirstOrDefaultAsync();
                    rhnetContext.UserRoles.Add(new ApplicationUserRole() { RoleId = role_1.Id, ClientId = client_1.Id, UserId = userFabio.Id });
                    rhnetContext.UserRoles.Add(new ApplicationUserRole() { RoleId = role_1.Id, ClientId = client_2.Id, UserId = userFabio.Id });
                    rhnetContext.UserRoles.Add(new ApplicationUserRole() { RoleId = role_2.Id, ClientId = client_2.Id, UserId = userFabio.Id });
                    await rhnetContext.SaveChangesAsync();
                }

                return Ok("Operação concluída");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();

                var erro = ex.EntityValidationErrors.ElementAt(0).Entry.Entity.ToString() + " ";
                for (var i = 0; i < ex.EntityValidationErrors.Count(); i++)
                {
                    for (var x = 0; x < ex.EntityValidationErrors.ElementAt(i).ValidationErrors.Count(); x++)
                    {
                        erro += ex.EntityValidationErrors.ElementAt(i).ValidationErrors.ElementAt(x).ErrorMessage + "\n";
                    }

                }
                return BadRequest(erro + " - Linha:" + line);
                
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();

                return BadRequest (ex.Message.ToString() + " - Linha:" + line);
                
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel model)
        {
            
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("grant_type", "password"));
            pairs.Add(new KeyValuePair<string, string>("username", model.Username));
            pairs.Add(new KeyValuePair<string, string>("Password", model.Password));
            pairs.Add(new KeyValuePair<string, string>("client_Id", "e84a2d13704647d18277966ec839d39e:CgP7NyLXtaGmyOgjj3sUMwmAlrSKqa5JyZ4P1OlfQeM"));
            if (model.SelectedClient != null)
            {
                pairs.Add(new KeyValuePair<string, string>("selectedClient", model.SelectedClient.Cnpj));
            }
            

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            HttpClient client = new HttpClient();
            var tokenServiceUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/api/Security/Token";
            var response = await client.PostAsync(tokenServiceUrl, content);

            string resultContent = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                var erros = Newtonsoft.Json.JsonConvert.DeserializeObject< Dictionary< string, string>>(resultContent);
                if(erros.Where(e => e.Value == "client_select_error").Count() > 0)
                {
                    var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var clients = await userManager.GetClientsAsync(model.Username);
                    return Content(HttpStatusCode.Conflict, clients);
                }
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

                if (clients.Count() > 1 && model.SelectedClient == null)
                {
                    return Content(HttpStatusCode.Conflict, clients);
                }

                ClientModel selectedClient = null;


                if (clients.Count() == 1)
                {
                    selectedClient = clients[0];
                }
                else
                {
                    for (var i = 0; i < clients.Count; i++)
                    {
                        if (model.SelectedClient.Cnpj == clients[i].Cnpj)
                        {
                            selectedClient = clients[i];
                            break;
                        }
                    }
                }

                if (selectedClient == null)
                {
                    return BadRequest("Cliente não associado a um cliente ou cliente selecionado incorreto");
                }

                if (selectedClient.Situation == Enums.ClientSituation.Bloqueado && user.UserName != "master")
                {
                    return BadRequest("Cliente bloqueado. Entre em contato com um administrador do sistema");
                }

                if (selectedClient.Situation == Enums.ClientSituation.Inativo && user.UserName != "master")
                {
                    return BadRequest("Cliente inativo. Entre em contato com um administrador do sistema");
                }


                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(resultContent);

                result.currentClient = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    id = selectedClient.Id,
                    cnpj = selectedClient.Cnpj,
                    description = selectedClient.Description,
                    situation = selectedClient.Situation
                });

                return Ok(result);
            }
        }


    }
}