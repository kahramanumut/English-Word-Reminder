using Microsoft.AspNetCore.Mvc;

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
