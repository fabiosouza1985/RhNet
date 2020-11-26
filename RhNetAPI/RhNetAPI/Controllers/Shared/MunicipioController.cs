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
    [Route("api/municipio")]
    public class MunicipioController : ControllerBase
    {
        [HttpGet]
        [Route("getProperties")]
        public ActionResult<List<Property>> GetProperties()
        {            
            return Ok(Property.GetProperties(typeof(MunicipioModel)));
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<MunicipioModel>>> Get([FromServices] RhNetContext rhNetContext)
        {
            MunicipioRepository repository = new MunicipioRepository();
            return await repository.Get(rhNetContext);

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromServices] RhNetContext rhNetContext, [FromBody] MunicipioModel municipioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MunicipioRepository repository = new MunicipioRepository();
            object result = await repository.Add(rhNetContext, municipioModel);

            if(result.GetType() == typeof(MunicipioModel))
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
        public async Task<ActionResult> Update([FromServices] RhNetContext rhNetContext, [FromBody] MunicipioModel municipioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MunicipioRepository repository = new MunicipioRepository();
            object result = await repository.Update(rhNetContext, municipioModel);

            if (result.GetType() == typeof(MunicipioModel))
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
        public async Task<ActionResult> Remove([FromServices] RhNetContext rhNetContext, [FromBody] MunicipioModel municipioModel)
        {
            
            MunicipioRepository repository = new MunicipioRepository();
            object result = await repository.Remove(rhNetContext, municipioModel);

            if (result.GetType() == typeof(MunicipioModel))
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
