﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Repositories.Adm;
using RhNetAPI.Models.Adm;
using Microsoft.AspNetCore.Authorization;
using RhNetAPI.Contexts;
using System.Security.Claims;
using RhNetAPI.Services;

namespace RhNetAPI.Controllers.Adm
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("getUserInfo")]
        public async Task<ActionResult<UserModel>> GetUserInfo([FromServices] UserManager<ApplicationUser> userManager)
        {
            if(this.User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(this.User.Identity.Name);

                if (user == null)
                {                    
                    return BadRequest("Usuário não encontrado");
                }
                UserModel userModel = new UserModel()
                {
                    Username = user.UserName,
                    Email = user.Email
                    
                };
                return Ok(userModel);
            }
            else
            {
                return BadRequest("Usuário não autenticado");
            }
        }

        [HttpPost]
        [Route("SetClient")]
        public async Task< ActionResult> SetClient([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, [FromBody] ClientModel client)
        {
            ApplicationUser user = await userManager.FindByNameAsync(this.User.Identity.Name);

            UserRepository repository = new UserRepository();

            var profiles = (await repository.GetRolesAsync(userManager, rhNetContext, this.User.Identity.Name, client.Id)).ToList();
            var claims = (await repository.GetClaimsAsync(userManager, rhNetContext, this.User.Identity.Name, client.Id)).ToList();

            var token = TokenService.GenerateToken(user, profiles, claims);

            var loginUser = new UserModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token,
                Profiles = profiles,
                CurrentClient = client
            };

            return Ok( loginUser);
        }

        [Authorize(Policy = "ViewUser")]
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<ApplicationUserModel>>> GetUsers([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager, [FromServices] RhNetContext rhNetContext)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetUsers(userManager, roleManager, rhNetContext, this.User.Identity.Name);

        }


        [Authorize(Policy = "AddUser")]
        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult<ApplicationUserModel>> AddUser([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, [FromBody] ApplicationUserModel applicationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                UserRepository repository = new UserRepository();
            var result = await repository.AddUserAsync(userManager, rhNetContext, applicationUserModel);

            if (result == applicationUserModel)
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
          

        }

       
        [HttpGet]
        [Route("getroles")]
        public async Task<ActionResult<List<RoleModel>>> GetRoles([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, int clientId)
        {
            UserRepository repository = new UserRepository();
           
            return await repository.GetRolesAsync(userManager, rhNetContext, this.User.Identity.Name, clientId);
           
        }

         [Authorize]
         [HttpGet]
        [Route("getallroles")]
        public async Task<ActionResult<List<RoleModel>>> GetAllRoles([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetAllRolesAsync(userManager, roleManager,  this.User.Identity.Name);
           
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addRole")]
        public async Task<ActionResult<RoleModel>> AddRole([FromServices] RoleManager<ApplicationRole> roleManager, [FromBody] RoleModel role)
        {
            UserRepository repository = new UserRepository();
            return await repository.AddRoleAsync(roleManager, role);

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateRole")]
        public async Task<ActionResult<RoleModel>> UpdateRole([FromServices] RoleManager<ApplicationRole> roleManager, [FromBody] RoleModel role)
        {
            UserRepository repository = new UserRepository();
            return await repository.UpdateRoleAsync(roleManager, role);

        }
    }
}
