using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
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
        [Route("isFavorite")]
        public async Task<ActionResult<bool>> IsFavorite([FromServices] RhNetContext rhNetContext, string path)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.IsFavorite(rhNetContext, this.User.Identity.Name, path);

        }

        [HttpPost]
        [Route("addFavorite")]
        public async Task<ActionResult<string>> AddFavorite([FromServices] RhNetContext rhNetContext,[FromBody] string path)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return await repository.AddFavorite(rhNetContext, this.User.Identity.Name, path);

        }
    }
}
