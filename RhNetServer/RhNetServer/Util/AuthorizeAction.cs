using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RhNetServer.Util
{
    public class AuthorizeAction : AuthorizeAttribute
    {
        string action;
        public AuthorizeAction(string action)
        {
            this.action = action;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        bool Authorize()
        {
            var user = (ClaimsPrincipal) HttpContext.Current.User;
            if (user.Identity.Name == "master") return true;

            return user.Claims.Where(e => e.Type == "permission" && e.Value == action).Count() > 0;
        }
    }
}