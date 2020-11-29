using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
using RhNetAPI.Models.Shared;
using RhNetAPI.Repositories.Shared;
using RhNetAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Controllers.Shared
{
    [Authorize]
    [ApiController]
    [Route("api/ato_normativo")]
    public class Ato_NormativoController : ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(Ato_NormativoModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<Ato_NormativoModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            Ato_NormativoRepository repository = new Ato_NormativoRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] Ato_NormativoModel ato_normativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ato_NormativoRepository repository = new Ato_NormativoRepository();
            object result = await repository.Add(rhNetContext, ato_normativoModel);

            if (result.GetType() == typeof(Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] Ato_NormativoModel ato_normativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ato_NormativoRepository repository = new Ato_NormativoRepository();
            object result = await repository.Update(rhNetContext, ato_normativoModel);

            if (result.GetType() == typeof(Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] Ato_NormativoModel ato_normativoModel)
        {

            Ato_NormativoRepository repository = new Ato_NormativoRepository();
            object result = await repository.Remove(rhNetContext, ato_normativoModel);

            if (result.GetType() == typeof(Ato_NormativoModel))
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("errors", result.ToString());
                return BadRequest(ModelState);
            }
        }
    }
}
