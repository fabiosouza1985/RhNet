using Microsoft.IdentityModel.Tokens;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Models.Adm;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RhNetAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(ApplicationUser user, List<RoleModel> profiles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");

            ClaimsIdentity subject = new ClaimsIdentity();
            subject.AddClaim(new Claim(ClaimTypes.Name, user.UserName.ToString()));
            subject.AddClaim(new Claim(ClaimTypes.Email, user.Email.ToString()));
            for(var i = 0; i < profiles.Count(); i++){
                subject.AddClaim(new Claim(ClaimTypes.Role, profiles[i].Name));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
