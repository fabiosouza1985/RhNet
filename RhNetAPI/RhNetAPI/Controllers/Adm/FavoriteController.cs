using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
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
        public async Task<ActionResult<List<FavoriteModel>>> GetFavorites([FromServices] RhNetContext rhNetContext, string profile)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.GetFavorites(rhNetContext, this.User.Identity.Name, profile);

        }

        [HttpGet]
        [Route("isFavorite")]
        public async Task<ActionResult<bool>> IsFavorite([FromServices] RhNetContext rhNetContext, string path)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.IsFavorite(rhNetContext, this.User.Identity.Name, path);

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
