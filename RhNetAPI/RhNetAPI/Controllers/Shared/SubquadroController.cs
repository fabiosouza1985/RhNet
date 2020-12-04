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
    [Route("api/subquadro")]
    public class SubquadroController : ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(SubquadroModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<SubquadroModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            SubquadroRepository repository = new SubquadroRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] SubquadroModel subquadroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubquadroRepository repository = new SubquadroRepository();
            object result = await repository.Add(rhNetContext, subquadroModel);

            if (result.GetType() == typeof(SubquadroModel))
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
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] SubquadroModel subquadroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubquadroRepository repository = new SubquadroRepository();
            object result = await repository.Update(rhNetContext, subquadroModel);

            if (result.GetType() == typeof(SubquadroModel))
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
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] SubquadroModel subquadroModel)
        {

            SubquadroRepository repository = new SubquadroRepository();
            object result = await repository.Remove(rhNetContext, subquadroModel);

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
