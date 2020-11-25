using Microsoft.AspNetCore.Authorization;
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

namespace RhNetAPI.Controllers.Adm
{
    [Authorize()]
    [ApiController]
    [Route("api/favorite")]
    public class FavoriteController : ControllerBase
    {
        [HttpGet]
        [Route("getFavorites")]
        public async Task<ActionResult<List<FavoriteModel>>> GetFavorites([FromServices] UserManager<ApplicationUser> userManager, [FromServices] RhNetContext rhNetContext, string profile, int clientId)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.GetFavorites(userManager, rhNetContext, this.User.Identity.Name, profile, clientId);

        }

        [HttpGet]
        [Route("isFavorite")]
        public async Task<ActionResult<bool>> IsFavorite([FromServices] RhNetContext rhNetContext, string path, string profile)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.IsFavorite(rhNetContext, this.User.Identity.Name, path, profile);

        }

        [HttpPost]
        [Route("addFavorite")]
        public async Task<ActionResult> AddFavorite([FromServices] RhNetContext rhNetContext,[FromBody] FavoriteModel favoriteModel)
        {
            FavoriteRepository repository = new FavoriteRepository();
            var result = await repository.AddFavorite(rhNetContext, this.User.Identity.Name, favoriteModel);

            if(result == favoriteModel)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("removeFavorite")]
        public async Task<ActionResult> RemoveFavorite([FromServices] RhNetContext rhNetContext, [FromBody] FavoriteModel favoriteModel)
        {
            FavoriteRepository repository = new FavoriteRepository();
            var result = await repository.RemoveFavorite(rhNetContext, this.User.Identity.Name, favoriteModel);

            if (result == favoriteModel)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
