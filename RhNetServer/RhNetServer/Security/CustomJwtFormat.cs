using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using Thinktecture.IdentityModel.Tokens;
using System.Security.Claims;

namespace RhNetServer.Security
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;
        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }
        public string Protect(AuthenticationTicket data)
        {

            if (data == null)
                throw new ArgumentNullException("data");


            string audience = data.Properties.Dictionary["audience"];
            if (string.IsNullOrWhiteSpace(audience)) throw new InvalidOperationException("ClientId e AccessKey não foi encontrado");
            var keys = audience.Split(':');
            var client_id = keys.First();
            var accessKey = keys.Last();
            var applicationAccess = WebApplicationAccess.Find(client_id);
            var keyByteArray = TextEncodings.Base64Url.Decode(applicationAccess.SecretKey);
            var signingKey = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256Signature);
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;
            var token = new JwtSecurityToken(_issuer, client_id, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);

            return jwt;

        }
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}