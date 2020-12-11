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
    [RoutePrefix("api/subquadro")]
    public class SubquadroController : ApiController
    {
        RhNetContext rhNetContext;
        SubquadroRepository repository;

        
        SubquadroController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new SubquadroRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(SubquadroModel)));
        }

        [AuthorizeAction("Visualizar Subquadros")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Adicionar Subquadro")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] SubquadroModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(SubquadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Subquadro")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] SubquadroModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(SubquadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Subquadro")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] SubquadroModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(SubquadroModel))
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