using Microsoft.AspNet.Identity.Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using RhNetServer.Models.Adm;
using RhNetServer.Repositories.Adm;
using RhNetServer.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RhNetServer.Controllers.Adm
{

    [Authorize]
    [RoutePrefix("api/client")]
    public class ClientController: ApiController
    {

       
        RhNetContext rhNetContext;
        ApplicationUserManager userManager;
        ClientRepository repository;
        ClientController()
        {
            
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            repository = new ClientRepository();
        }

        [AuthorizeAction("Visualizar Clientes")]
        [Authorize(Roles ="Master")]
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<List<ClientModel>> GetAllClients()
        {
            ClientRepository clientRepository = new ClientRepository();           
            return await clientRepository.GetAllClients(rhNetContext);
        }

      
        [HttpGet]
        [Route("getClients")]
        public async Task<List<ClientModel>> GetClients()
        {           
            return await repository.GetClients(rhNetContext, userManager, this.User.Identity.Name);
        }

        [AuthorizeAction("Adicionar Cliente")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("addClient")]
        public async Task<IHttpActionResult> AddClient([FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {               
                return Ok(await repository.AddClient(clientModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [AuthorizeAction("Atualizar Cliente")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("updateClient")]
        public async Task<IHttpActionResult> UpdateClient([FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {          
                return Ok( await repository.UpdateClient(clientModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}