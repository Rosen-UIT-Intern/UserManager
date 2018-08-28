using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManager.Contract;
using UserManager.Contract.DTOs;

namespace UserManager.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/user/light")]
    public class LightUserController : Controller
    {
        private readonly IUserService _service;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public LightUserController(IUserService service, IRandomIdGenerator randomIdGenerator)
        {
            _service = service;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetLightUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _service.GetLightUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }
    }
}
