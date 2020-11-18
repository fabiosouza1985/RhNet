using Microsoft.AspNetCore.Mvc;
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

namespace RhNetAPI.Controllers.Adm
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [Authorize(Policy = "ViewUser")]
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<ApplicationUserModel>>> GetUsers([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager, [FromServices] RhNetContext rhNetContext)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetUsers(userManager, roleManager, rhNetContext, this.User.Identity.Name);

        }


        [HttpGet]
        [Route("getroles")]
        public async Task<ActionResult<List<RoleModel>>> GetRoles([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetRolesAsync(userManager, roleManager, this.User.Identity.Name);
           
        }

         [Authorize(Roles ="Master")]
         [HttpGet]
        [Route("getallroles")]
        public async Task<ActionResult<List<RoleModel>>> GetAllRoles( [FromServices] RoleManager<ApplicationRole> roleManager)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetAllRolesAsync(roleManager);
           
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
