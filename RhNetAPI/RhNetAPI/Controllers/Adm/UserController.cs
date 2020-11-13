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

namespace RhNetAPI.Controllers.Adm
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

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
    }
}
