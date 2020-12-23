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
    [RoutePrefix("api/quadro")]
    public class QuadroController : ApiController
    {
        RhNetContext rhNetContext;
        QuadroRepository repository;

        QuadroController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            repository = new QuadroRepository();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(QuadroModel)));
        }

        [AuthorizeAction("Visualizar Quadros")]
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {           
            return Ok(await repository.Get(rhNetContext));

        }

        [AuthorizeAction("Visualizar Quadros")]
        [HttpGet]
        [Route("getById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await repository.Get(rhNetContext, id);

            if(result == null)
            {
                return BadRequest("Quadro não encontrado");
            }

            return Ok((QuadroModel) result);

        }

        [AuthorizeAction("Adicionar Quadro")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] QuadroModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Add(rhNetContext, model);

            if (result.GetType() == typeof(QuadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Quadro")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] QuadroModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.Update(rhNetContext, model);

            if (result.GetType() == typeof(QuadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Remover Quadro")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] QuadroModel model)
        {

            object result = await repository.Remove(rhNetContext, model);

            if (result.GetType() == typeof(QuadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Quadro")]
        [HttpPost]
        [Route("addAtoNormativo")]
        public async Task<IHttpActionResult> AddAtoNormativo([FromBody] Quadro_Ato_NormativoModel quadro_Ato_NormativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.AddAtoNormativo(rhNetContext, quadro_Ato_NormativoModel);

            if (result.GetType() == typeof(Quadro_Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [AuthorizeAction("Atualizar Quadro")]
        [HttpPost]
        [Route("removeAtoNormativo")]
        public async Task<IHttpActionResult> RemoveAtoNormativo([FromBody] Quadro_Ato_NormativoModel quadro_Ato_NormativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object result = await repository.RemoveAtoNormativo(rhNetContext, quadro_Ato_NormativoModel);

            if (result.GetType() == typeof(Quadro_Ato_NormativoModel))
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