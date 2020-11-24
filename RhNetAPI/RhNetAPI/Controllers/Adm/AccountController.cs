using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Models.Adm;
using RhNetAPI.Services;
using RhNetAPI.Repositories.Adm;
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
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager, [FromBody] LoginModel model)
        {
           
            ApplicationUser user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                if(model.Username == "master" && model.Password == "Mm123456*")
                {
                    await userManager.CreateAsync(new ApplicationUser() { Email = "master@email.com", UserName = "master", Cpf = "11111111111" }, "Mm123456*");
                    user = await userManager.FindByNameAsync(model.Username);

                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Master", Description = "Acesso Total ao sistema. Pode cadastrar novos usuários, clientes e empresas." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Administrador", Description = "Acesso e cadastro das tabelas em comum do sistema. Pode cadastrar Super Usuários." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Super Usuário", Description = "Cadastra Usuário do RH." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "RH", Description = "Cadastra funcionários, atualiza dados dos funcionários e tabelas específicas." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Gestor Mediato", Description = "Responsável por autorizar as solicitações do Superior Mediato." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Gestor Imediato", Description = "Responsável por autorizar as solicitações de seus suborinados, lançamento de férias, entre outros." });
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "Funcionário", Description = "Consulta informações próprias no sistema, entre outros." });

                    await userManager.AddToRoleAsync(user, "Master");
                    await userManager.AddToRoleAsync(user, "Administrador");
                    await userManager.AddToRoleAsync(user, "Super Usuário");
                    await userManager.AddToRoleAsync(user, "RH");
                    await userManager.AddToRoleAsync(user, "Gestor Mediato");
                    await userManager.AddToRoleAsync(user, "Gestor Imediato");
                    await userManager.AddToRoleAsync(user, "Funcionário");

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
            
            UserRepository repository = new UserRepository();
            var profiles =  (await repository.GetRolesAsync(userManager, roleManager, user.UserName)).ToList();
            // Gera o Token
            var token = TokenService.GenerateToken(user, profiles);

            
            var loginUser = new UserModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token,
                Profiles = profiles
            };

            return StatusCode((int)HttpStatusCode.OK, loginUser);
        }

        

    }
}
