using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using UserManager.Contract;

namespace UserManager.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetRoles());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!Guid.TryParse(id, out Guid roleId))
            {
                return BadRequest(id);
            }

            var org = _service.GetRole(roleId);

            if (org == null)
            {
                return NotFound(id);
            }

            return Json(org);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUser(string id)
        {
            if (!Guid.TryParse(id, out Guid roleId))
            {
                return BadRequest(id);
            }

            var org = _service.GetUsers(roleId);

            if (org == null)
            {
                return NotFound(id);
            }

            return Json(org);
        }
    }
}
