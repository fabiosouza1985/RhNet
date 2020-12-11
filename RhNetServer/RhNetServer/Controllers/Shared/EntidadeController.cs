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
    [RoutePrefix("api/entidade")]
    public class EntidadeController : ApiController
    {

        RhNetContext rhNetContext;
        EntidadeRepository repository;

        EntidadeController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new EntidadeRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(EntidadeModel)));
        }

        [AuthorizeAction("Visualizar Entidades")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Adicionar Entidade")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] EntidadeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(EntidadeModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Entidade")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] EntidadeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(EntidadeModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Entidade")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] EntidadeModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(EntidadeModel))
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