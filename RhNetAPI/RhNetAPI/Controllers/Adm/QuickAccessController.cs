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
    [Route("api/quickaccess")]
    public class QuickAccessController : ControllerBase
    {

        [Authorize(Roles = "Master")]
        [HttpGet]
        [Route("getAllQuickAccess")]
        public async Task<ActionResult<List<QuickAccessModel>>> GetAllQuickAccess([FromServices] RhNetContext rhNetContext)
        {
            QuickAccessRepository repository = new QuickAccessRepository();
            return await repository.GetAllQuickAccess(rhNetContext);

        }


        [HttpGet]
        [Route("getQuickAccess")]
        public async Task<ActionResult<List<QuickAccessModel>>> GetQuickAccess([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, string profile)
        {
            QuickAccessRepository repository = new QuickAccessRepository();
            return await repository.GetQuickAccess(this.User.Identity.Name, profile, rhNetContext, userManager);

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addQuickAccess")]
        public async Task<ActionResult<QuickAccessModel>> AddQuickAccess([FromServices] RhNetContext rhNetContext, [FromBody] QuickAccessModel quickAccessModel)
        {
            if (ModelState.IsValid)
            {
                QuickAccessRepository repository = new QuickAccessRepository();
                return await repository.AddQuickAccess(quickAccessModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateQuickAccess")]
        public async Task<ActionResult<QuickAccessModel>> UpdateQuickAccess([FromServices] RhNetContext rhNetContext, [FromBody] QuickAccessModel quickAccessModel)
        {
            if (ModelState.IsValid)
            {
                QuickAccessRepository repository = new QuickAccessRepository();
                return await repository.UpdateQuickAccess(quickAccessModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("removeQuickAccess")]
        public async Task<ActionResult<QuickAccessModel>> RemoveQuickAccess([FromServices] RhNetContext rhNetContext, [FromBody] QuickAccessModel quickAccessModel)
        {
            if (ModelState.IsValid)
            {
                QuickAccessRepository repository = new QuickAccessRepository();
                return await repository.RemoveQuickAccess(quickAccessModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
