using Microsoft.AspNet.Identity.Owin;
using RhNetServer.Contexts;
using RhNetServer.Models.Shared;
using RhNetServer.Repositories.Shared;
using RhNetServer.Util;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RhNetServer.Controllers.Shared
{
    [Authorize]
    [RoutePrefix("api/municipio")]
    public class MunicipioController : ApiController
    {
        RhNetContext rhNetContext;
        MunicipioRepository repository;
        MunicipioController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new MunicipioRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(MunicipioModel)));
        }

        [AuthorizeAction("Visualizar Municípios")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Adicionar Município")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] MunicipioModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(MunicipioModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Município")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] MunicipioModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(MunicipioModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Município")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] MunicipioModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(MunicipioModel))
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