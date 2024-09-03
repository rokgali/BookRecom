using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        public async Task<IActionResult> UserTestThing()
        {
            return Ok("Test thing work");
        }
    }
}