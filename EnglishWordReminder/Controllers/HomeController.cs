using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishWordReminder.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnglishWordReminder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
           
            return Ok("Bot Works");
        }
    }
}
