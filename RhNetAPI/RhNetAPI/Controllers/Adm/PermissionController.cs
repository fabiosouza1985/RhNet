﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
using RhNetAPI.Models.Adm;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RhNetAPI.Util;

namespace RhNetAPI.Controllers.Adm
{
    [Authorize()]
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {

        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(PermissionModel)));
        }

        [Authorize(Roles = "Master", Policy = "ViewPermission")]
        [HttpGet]
        [Route("getAllPermissions")]
        public async Task<ActionResult<List<PermissionModel>>> GetAllPermissions([FromServices] RhNetContext rhNetContext)
        {
            PermissionRepository repository = new PermissionRepository();
            return await repository.GetAllPermissions(rhNetContext);

        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<PermissionModel>>> GetPermissions([FromServices] RhNetContext rhNetContext, [FromServices] UserManager<ApplicationUser> userManager)
        {
            PermissionRepository repository = new PermissionRepository();
            return await repository.GetPermissions(rhNetContext, userManager, this.User.Identity.Name);

        }

        [Authorize(Roles = "Master", Policy = "AddPermission")]
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<PermissionModel>> AddPermission([FromServices] RhNetContext rhNetContext, [FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return await repository.AddPermission(permissionModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master", Policy = "UpdatePermission")]
        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<PermissionModel>> UpdatePermission([FromServices] RhNetContext rhNetContext, [FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return await repository.UpdatePermission(permissionModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master", Policy = "RemovePermission")]
        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult<PermissionModel>> RemovePermission([FromServices] RhNetContext rhNetContext, [FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return await repository.RemovePermission(permissionModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
