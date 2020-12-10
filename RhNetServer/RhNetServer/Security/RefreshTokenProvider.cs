using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using RhNetServer.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Extensions;

namespace RhNetServer.Security
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            //var data = await context.Request.ReadFormAsync();

            //var selectedClient = data.Where(x => x.Key == "selectedClient").Select(x => x.Value).LastOrDefault().LastOrDefault();

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //var roles = (await userManager.GetRoleByClientAsync(context.Ticket.Identity.Claims.GetValue(ClaimTypes.Name), selectedClient)).Select(e => new { name = e.Name, description = e.Description });
            
            //var claims = await userManager.GetClaimsAsync(context.Ticket.Identity.Claims.GetValue(ClaimTypes.Name), selectedClient);
            
            //foreach (Claim claim in context.Ticket.Identity.Claims)
            //{
            //    if (claim.Type == ClaimTypes.Role)
            //    {
            //        if (roles.Where(e => e.name == claim.Value).Count() == 0)
            //        {
            //            context.Ticket.Identity.RemoveClaim(claim);
            //        }
            //    }

            //    if (claim.Type == "permission")
            //    {
            //        if (claims.Where(e => e.Value == claim.Value).Count() == 0)
            //        {
            //            context.Ticket.Identity.RemoveClaim(claim);
            //        }
            //    }
            //}

            //context.Ticket.Properties.Dictionary.Remove("profiles");
            //context.Ticket.Properties.Dictionary.Add("profiles", Newtonsoft.Json.JsonConvert.SerializeObject(roles));
            
            //for (var i = 0; i < roles.Count(); i++)
            //{
            //    if (context.Ticket.Identity.Claims.Where(e => e.Type == ClaimTypes.Role && e.Value == roles.ElementAt(i).name).Count() == 0)
            //    {
            //        context.Ticket.Identity.AddClaim(new Claim(ClaimTypes.Role, roles.ElementAt(i).name));
            //    }


            //}

            //for (var i = 0; i < claims.Count(); i++)
            //{
            //    if (context.Ticket.Identity.Claims.Where(e => e.Type == claims.ElementAt(i).Type && e.Value == claims.ElementAt(i).Value).Count() == 0)
            //    {
            //        context.Ticket.Identity.AddClaim(new Claim(claims[i].Type, claims[i].Value));
            //    }


            //}
            Create(context);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            Receive(context);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            object inputs;
            context.OwinContext.Environment.TryGetValue("Microsoft.Owin.Form#collection", out inputs);

            
            var grantType = ((FormCollection)inputs)?.GetValues("grant_type");
            var client = ((FormCollection)inputs)?.GetValues("selectedClient");

            var grant = grantType.FirstOrDefault();

            if (grant == null || grant.Equals("refresh_token")) return;

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddHours(1);
           
            context.SetToken(context.SerializeTicket());
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);

            if (context.Ticket == null)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                context.Response.ReasonPhrase = "invalid token";
                return;
            }

            if (context.Ticket.Properties.ExpiresUtc <= DateTime.UtcNow)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                context.Response.ReasonPhrase = "unauthorized";
                return;
            }

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddHours(1);
            context.SetTicket(context.Ticket);
        }
    }
}