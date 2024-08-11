using Microsoft.AspNetCore.Mvc;

namespace MyGeminiAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok("I'm up and running !!");
        }
    }
}