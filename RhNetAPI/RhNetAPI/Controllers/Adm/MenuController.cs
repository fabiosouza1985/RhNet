using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Models.Adm;
using RhNetAPI.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace RhNetAPI.Controllers.Adm
{
    [Authorize()]
    [ApiController]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {

        [Authorize(Roles ="Master")]
        [HttpGet]
        [Route("getAllMenus")]
        public async Task<ActionResult<List<MenuModel>>> GetAllMenus( [FromServices] RhNetContext rhNetContext)
        {
            MenuRepository repository = new MenuRepository();
            return await repository.GetAllMenus( rhNetContext);

        }

         
        [HttpGet]
        [Route("getMenus")]
        public async Task<ActionResult<List<MenuModel>>> GetMenus([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, string profile)
        {
            MenuRepository repository = new MenuRepository();
            return await repository.GetMenus(this.User.Identity.Name, profile, rhNetContext, userManager);

        }

        [HttpGet]
        [Route("getQuickAccess")]
        public async Task<ActionResult<List<MenuModel>>> GetQuickAccess([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, string profile)
        {
            MenuRepository repository = new MenuRepository();
            return await repository.GetQuickAccess(this.User.Identity.Name, profile, rhNetContext, userManager);

        }

        [Authorize(Roles ="Master")]
        [HttpPost]
        [Route("addMenu")]
        public async Task<ActionResult<MenuModel>> AddMenu( [FromServices] RhNetContext rhNetContext, [FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return await repository.AddMenu(menuModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }          

        }
        public override ForbidResult Forbid()
        {
            return base.Forbid("teste");
        }
        [Authorize(Roles ="Master", Policy = "ViewUsers")]
        [HttpPost]
        [Route("updateMenu")]
        public async Task<ActionResult<MenuModel>> UpdateMenu( [FromServices] RhNetContext rhNetContext, [FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return await repository.UpdateMenu(menuModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }          

        }

        [Authorize(Roles ="Master")]
        [HttpPost]
        [Route("removeMenu")]
        public async Task<ActionResult<MenuModel>> RemoveMenu( [FromServices] RhNetContext rhNetContext, [FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return await repository.RemoveMenu(menuModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }          

        }

    }
}
