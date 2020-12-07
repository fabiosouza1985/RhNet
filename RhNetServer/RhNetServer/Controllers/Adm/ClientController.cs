using Microsoft.AspNet.Identity.Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RhNetServer.Controllers.Adm
{

   
    [RoutePrefix("api/client")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController: ApiController
    {

        [Authorize(Roles ="Master")]
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<List<ClientModel>> GetAllClients()
        {
            ClientRepository clientRepository = new ClientRepository();
            RhNetContext rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            return await clientRepository.GetAllClients(rhNetContext);
        }

      [Authorize]
        [HttpGet]
        [Route("getClients")]
        public async Task<List<ClientModel>> GetClients()
        {
            ClientRepository repository = new ClientRepository();
            RhNetContext rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return await repository.GetClients(rhNetContext, userManager, this.User.Identity.Name);
        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addClient")]
        public async Task<IHttpActionResult> AddClient([FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                ClientRepository repository = new ClientRepository();
                RhNetContext rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();               

                return Ok(await repository.AddClient(clientModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateClient")]
        public async Task<IHttpActionResult> UpdateClient([FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                ClientRepository repository = new ClientRepository();
                RhNetContext rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
                return Ok( await repository.UpdateClient(clientModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}