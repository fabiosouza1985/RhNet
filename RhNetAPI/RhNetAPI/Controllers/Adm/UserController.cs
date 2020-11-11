using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Repositories.Adm;
using RhNetAPI.Models.Adm;

namespace RhNetAPI.Controllers.Adm
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("getroles")]
        public async Task<ActionResult<List<RoleModel>>> GetProfiles([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<ApplicationRole> roleManager)
        {
            UserRepository repository = new UserRepository();
            return await repository.GetRolesAsync(userManager, roleManager, this.User.Identity.Name);
           
        }
    }
}
