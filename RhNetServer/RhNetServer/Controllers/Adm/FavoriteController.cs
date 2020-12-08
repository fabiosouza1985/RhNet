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

namespace RhNetServer.Controllers.Adm
{
    [Authorize]
    [RoutePrefix("api/favorite")]
    public class FavoriteController : ApiController

    {
        RhNetContext rhNetContext;
        ApplicationUserManager userManager;
        FavoriteController()
        {
             rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
             userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
       
        [Route("getFavorites")]
        public async Task<IHttpActionResult> GetFavorites(string profile,  int clientId)
        {
            FavoriteRepository repository = new FavoriteRepository();
           

            return Ok(await repository.GetFavorites(userManager, rhNetContext, this.User.Identity.Name, profile, clientId));
        }

        [HttpGet]
        [Route("isFavorite")]
        public async Task<IHttpActionResult> IsFavorite(string path, string profile)
        {
            FavoriteRepository repository = new FavoriteRepository();
            return Ok(await repository.IsFavorite(rhNetContext, this.User.Identity.Name, path, profile));
        }

        [HttpPost]
        [Route("addFavorite")]
        public async Task<IHttpActionResult> AddFavorite([FromBody] FavoriteModel favoriteModel)
        {
            FavoriteRepository repository = new FavoriteRepository();
            var result = await repository.AddFavorite(rhNetContext, this.User.Identity.Name, favoriteModel);

            if (result == favoriteModel)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [HttpPost]
        [Route("removeFavorite")]
        public async Task<IHttpActionResult> RemoveFavorite([FromBody] FavoriteModel favoriteModel)
        {
            FavoriteRepository repository = new FavoriteRepository();
            var result = await repository.RemoveFavorite(rhNetContext, this.User.Identity.Name, favoriteModel);

            if (result == favoriteModel)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }
    }
}