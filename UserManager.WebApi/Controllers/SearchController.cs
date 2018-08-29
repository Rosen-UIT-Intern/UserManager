using System;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using UserManager.Contract;
using UserManager.Contract.DTOs;

namespace UserManager.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _service;

        public SearchController(ISearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] QuerryDTO querry)
        {
            
            return Json(_service.Search(querry));
        }
    }
}
