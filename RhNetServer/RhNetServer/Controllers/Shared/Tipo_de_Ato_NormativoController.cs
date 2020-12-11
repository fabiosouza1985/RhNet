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
    [RoutePrefix("api/tipo_de_ato_normativo")]
    public class Tipo_de_Ato_NormativoController : ApiController
    {
        RhNetContext rhNetContext;
        Tipo_de_Ato_NormativoRepository repository;
        Tipo_de_Ato_NormativoController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new Tipo_de_Ato_NormativoRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(Tipo_de_Ato_NormativoModel)));
        }

        [AuthorizeAction("Visualizar Tipos de Ato Normativo")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Adicionar Tipo de Ato Normativo")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] Tipo_de_Ato_NormativoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Tipo de Ato Normativo")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] Tipo_de_Ato_NormativoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Tipo de Ato Normativo")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] Tipo_de_Ato_NormativoModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
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