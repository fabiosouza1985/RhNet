using Microsoft.AspNet.Identity.Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RhNetServer.Util;

namespace RhNetServer.Controllers.Adm
{
    [Authorize]
    [RoutePrefix("api/Menu")]
    public class MenuController: ApiController
    {
        RhNetContext rhNetContext;
        ApplicationUserManager userManager;
        MenuController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        [AuthorizeAction("Visualizar Menus")]
        [Authorize(Roles = "Master")]
        [HttpGet]
        [Route("getAllMenus")]
        public async Task <IHttpActionResult> GetAllMenus()
        {
            MenuRepository repository = new MenuRepository();
            return Ok(await repository.GetAllMenus(rhNetContext));
        }

        
        [Route("getMenus")]
        public async Task<IHttpActionResult> GetMenus(string profile, int clientId)
        {
            MenuRepository repository = new MenuRepository();
            return Ok(await repository.GetMenus(this.User.Identity.Name, profile, rhNetContext, userManager, clientId));
        }

        [HttpGet]
        [Route("getQuickAccess")]
        public async Task<IHttpActionResult> GetQuickAccess(string profile, int clientId)
        {
            MenuRepository repository = new MenuRepository();
            return Ok(await repository.GetQuickAccess(this.User.Identity.Name, profile, rhNetContext, userManager, clientId));

        }

        [AuthorizeAction("Adicionar Menu")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addMenu")]
        public async Task<IHttpActionResult> AddMenu([FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return Ok(await repository.AddMenu(menuModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [AuthorizeAction("Atualizar Menu")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateMenu")]
        public async Task<IHttpActionResult> UpdateMenu([FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return Ok(await repository.UpdateMenu(menuModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [AuthorizeAction("Remover Menu")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("removeMenu")]
        public async Task<IHttpActionResult> RemoveMenu([FromBody] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuRepository repository = new MenuRepository();
                return Ok(await repository.RemoveMenu(menuModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}