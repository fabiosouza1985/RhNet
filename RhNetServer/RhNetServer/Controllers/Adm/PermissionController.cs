using RhNetServer.Models.Adm;
using System.Web;
using System.Web.Http;
using RhNetServer.Util;
using RhNetServer.Contexts;
using Microsoft.AspNet.Identity.Owin;
using RhNetServer.Repositories.Adm;
using System.Threading.Tasks;
using RhNetServer.App_Start;

namespace RhNetServer.Controllers.Adm
{
    
    [Authorize]
    [RoutePrefix("api/permission")]
    public class PermissionController : ApiController
    {

        RhNetContext rhNetContext;
        ApplicationUserManager userManager;
        PermissionController()
        {
            rhNetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        [HttpGet]
        [Route("getProperties")]
        public IHttpActionResult GetProperties()
        {
            return Ok(Property.GetProperties(typeof(PermissionModel)));
        }

        [AuthorizeAction("Visualizar Permissões")]
        [Authorize(Roles = "Master")]
        [HttpGet]
        [Route("getAll")]
        public async Task <IHttpActionResult> GetAllPermissions()
        {
            PermissionRepository repository = new PermissionRepository();
            return Ok(await repository.GetAllPermissions(rhNetContext));

        }

        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            PermissionRepository repository = new PermissionRepository();
            return Ok(await repository.GetPermissions(rhNetContext, userManager, this.User.Identity.Name));

        }

        [AuthorizeAction("Adicionar Permissão")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add([FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return Ok(await repository.AddPermission(permissionModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [AuthorizeAction("Atualizar Permissão")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return Ok(await repository.UpdatePermission(permissionModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [AuthorizeAction("Remover Permissão")]
        [Authorize(Roles = "Master")]
        [HttpPost]
        [Route("remove")]
        public async Task<IHttpActionResult> Remove([FromBody] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                PermissionRepository repository = new PermissionRepository();
                return Ok(await repository.RemovePermission(permissionModel, rhNetContext));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}