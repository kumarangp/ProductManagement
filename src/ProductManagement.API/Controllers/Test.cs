using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase {
        [HttpGet]
        public IActionResult Get() {
            Console.WriteLine("Test API endpoint was called!");
            return Ok("Hello from Test API!");
        }
    }
}
