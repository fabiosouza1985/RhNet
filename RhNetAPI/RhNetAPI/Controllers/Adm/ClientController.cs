using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RhNetAPI.Contexts;
using RhNetAPI.Models.Adm;
using RhNetAPI.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Controllers.Adm
{
    [Authorize]
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {

        [Authorize(Roles = "Master", Policy = "ViewClient")]
        [HttpGet]
        [Route("getAllClients")]
        public async Task<List<ClientModel>> GetAllClients([FromServices] RhNetContext rhNetContext)
        {
            ClientRepository repository = new ClientRepository();
            return await repository.GetAllClients(rhNetContext);
        }

        [Authorize(Roles = "Master", Policy = "AddClient")]
        [HttpPost]
        [Route("addClient")]
        public async Task<ActionResult<ClientModel>> AddClient([FromServices] RhNetContext rhNetContext, [FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                ClientRepository repository = new ClientRepository();
                return await repository.AddClient(clientModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master", Policy = "UpdateClient")]
        [HttpPost]
        [Route("updateClient")]
        public async Task<ActionResult<ClientModel>> UpdateClient([FromServices] RhNetContext rhNetContext, [FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                ClientRepository repository = new ClientRepository();
                return await repository.UpdateClient(clientModel, rhNetContext);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

       
    }
}
