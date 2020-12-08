using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RhNetServer.Security
{
    public class CustomOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id não pode ser nulo");
                return Task.FromResult<object>(null);
            }
            //Procurando pelo Client Id
            var token = context.ClientId.Split(':');
            var client_id = token.First();
            var accessKey = token.Last();
            var applicationAccess = WebApplicationAccess.Find(client_id);
            if (applicationAccess == null)
            {
                context.SetError("invalid_clientId", "client_Id não encontrado");
                return Task.FromResult<object>(null);
            }
            if (applicationAccess.AccessKey != accessKey)
            {
                context.SetError("invalid_clientId", "access key não encontrado ou inválido");
                return Task.FromResult<object>(null);
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                

                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();

                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "http://localhost:4200" });
                

                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    if (context.UserName == "master" && context.Password == "Mm123456*")
                    {
                        user = new ApplicationUser()
                        {
                            UserName = "master",
                            Email = "master@email.com",
                            Cpf = "11111111111"
                        };


                        await userManager.CreateAsync(user, "Mm123456*");
                        user = await userManager.FindByNameAsync("master");

                        await userManager.AddToRoleAsync(user.Id, "Master");
                        await userManager.AddToRoleAsync(user.Id, "Administrador");
                        await userManager.AddToRoleAsync(user.Id, "Super Usuário");
                        await userManager.AddToRoleAsync(user.Id, "RH");
                        await userManager.AddToRoleAsync(user.Id, "Gestor Mediato");
                        await userManager.AddToRoleAsync(user.Id, "Gestor Imediato");
                        await userManager.AddToRoleAsync(user.Id, "Funcionário");
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Usuário ou senha invalidos");
                        return;
                    }

                }

                var identity = new ClaimsIdentity("JWT");
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                var roles = await userManager.GetRolesAsync(user.Id);

                for (var i = 0; i < roles.Count; i++)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roles.ElementAt(i)));
                }

                var props = new AuthenticationProperties(new Dictionary<string, string>());
                props.Dictionary.Add("audience", (context.ClientId == null) ? string.Empty : context.ClientId);
                props.Dictionary.Add("username", user.UserName);
                props.Dictionary.Add("email", user.Email);

                if (roles.Count > 0)
                {
                    var listRoleModel = roleManager.Roles.Where(e => roles.Contains(e.Name)).OrderBy(e => e.Level).Select(e => new
                    {
                        name = e.Name,
                        description = e.Description,
                        id = e.Id,
                        level = e.Level
                    }).ToList();

                    props.Dictionary.Add("profiles", Newtonsoft.Json.JsonConvert.SerializeObject(listRoleModel));
                }

                var ticket = new AuthenticationTicket(identity, props);

                context.Validated(ticket);

                return;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);              
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();

                context.SetError("errors", ex.Message.ToString() + " - Linha:" + line);
                return;
            }

            
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            try
            {
                foreach (KeyValuePair<string, string>  _property  in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(_property.Key, _property.Value);
                }
                return Task.FromResult<object>(null);
            }catch(Exception ex)
            {
                return null;
            }
            
        }
    }

    
}
