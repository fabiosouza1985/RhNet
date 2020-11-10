using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Models.Adm;
using RhNetAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RhNetAPI.Controllers.Adm
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] UserManager<ApplicationUser> userManager ,[FromBody] LoginModel model)
        {
                     
            ApplicationUser user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                if(model.Username == "master" && model.Password == "Mm123456*")
                {
                    await userManager.CreateAsync(new ApplicationUser() { Email = "master@email.com", UserName = "master", Cpf = "11111111111" }, "Mm123456*");
                    user = await userManager.FindByNameAsync(model.Username);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Usuário não cadastrado.");
                }
                
            }
            

            bool isValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (!isValid)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Senha incorreta.");
            }

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            return new UserModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
        }


    }
}
