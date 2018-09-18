using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManager.Contract;
using UserManager.Contract.DTOs;
using UserManager.WebApi.ValidateFilter;

namespace UserManager.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/user")]
    [ValidateModel]
    public class FullUserController : Controller
    {
        private readonly IUserService _service;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public FullUserController(IUserService service, IRandomIdGenerator randomIdGenerator)
        {
            _service = service;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }

        [HttpPost]
        [Produces("text/plain")]
        public IActionResult CreateUser([FromBody] FrontendUserDTO dto)
        {
            string result = null;
            try
            {
                result = _service.Create(dto);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            return Ok(result);
        }

        [HttpPut]
        [Produces("text/plain")]
        public IActionResult UpdateUser([FromBody] FrontendUserDTO dto)
        {
            string result = null;
            try
            {
                result = _service.Update(dto);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (KeyNotFoundException knfex)
            {
                return NotFound(dto.Id);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Produces("text/plain")]
        public IActionResult DeleteUser(string id)
        {
            var result = _service.Delete(id);
            if (result)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
