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
    [RoutePrefix("api/ato_normativo")]
    public class Ato_NormativoController: ApiController
    {
        RhNetContext rhNetContext;
        Ato_NormativoRepository repository;

        Ato_NormativoController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new Ato_NormativoRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(Ato_NormativoModel)));
        }

        [AuthorizeAction("Visualizar Atos Normativos")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Adicionar Ato Normativo")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] Ato_NormativoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Ato Normativo")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] Ato_NormativoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Ato Normativo")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] Ato_NormativoModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(Ato_NormativoModel))
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