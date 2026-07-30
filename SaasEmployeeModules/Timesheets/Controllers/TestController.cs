using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Timesheets API is running");
        }
    }
}