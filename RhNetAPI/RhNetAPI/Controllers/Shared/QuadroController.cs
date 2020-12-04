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
    [Route("api/quadro")]
    public class QuadroController : ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(QuadroModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<QuadroModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            QuadroRepository repository = new QuadroRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] QuadroModel quadroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuadroRepository repository = new QuadroRepository();
            object result = await repository.Add(rhNetContext, quadroModel);

            if (result.GetType() == typeof(QuadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] QuadroModel quadroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuadroRepository repository = new QuadroRepository();
            object result = await repository.Update(rhNetContext, quadroModel);

            if (result.GetType() == typeof(QuadroModel))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] QuadroModel quadroModel)
        {

            QuadroRepository repository = new QuadroRepository();
            object result = await repository.Remove(rhNetContext, quadroModel);

            if (result.GetType() == typeof(QuadroModel))
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
