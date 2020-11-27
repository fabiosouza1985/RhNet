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
    [Route("api/entidade")]
    public class EntidadeController: ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {
            return Ok(Property.GetProperties(typeof(EntidadeModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<EntidadeModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            EntitadeRepository repository = new EntitadeRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] EntidadeModel entidadeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EntitadeRepository repository = new EntitadeRepository();
            object result = await repository.Add(rhNetContext, entidadeModel);

            if (result.GetType() == typeof(EntidadeModel))
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
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] EntidadeModel entidadeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EntitadeRepository repository = new EntitadeRepository();
            object result = await repository.Update(rhNetContext, entidadeModel);

            if (result.GetType() == typeof(EntidadeModel))
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
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] EntidadeModel entidadeModel)
        {

            EntitadeRepository repository = new EntitadeRepository();
            object result = await repository.Remove(rhNetContext, entidadeModel);

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
