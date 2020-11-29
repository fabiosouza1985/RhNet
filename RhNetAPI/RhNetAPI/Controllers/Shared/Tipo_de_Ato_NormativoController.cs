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
    [Route("api/tipo_de_ato_normativo")]
    public class Tipo_de_Ato_NormativoController : ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(Tipo_de_Ato_NormativoModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<Tipo_de_Ato_NormativoModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            Tipo_de_Ato_NormativoRepository repository = new Tipo_de_Ato_NormativoRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipo_de_Ato_NormativoRepository repository = new Tipo_de_Ato_NormativoRepository();
            object result = await repository.Add(rhNetContext, tipo_de_ato_normativoModel);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
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
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipo_de_Ato_NormativoRepository repository = new Tipo_de_Ato_NormativoRepository();
            object result = await repository.Update(rhNetContext, tipo_de_ato_normativoModel);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
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
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {

            Tipo_de_Ato_NormativoRepository repository = new Tipo_de_Ato_NormativoRepository();
            object result = await repository.Remove(rhNetContext, tipo_de_ato_normativoModel);

            if (result.GetType() == typeof(Tipo_de_Ato_NormativoModel))
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
