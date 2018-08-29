﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManager.WebApi
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValueController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
