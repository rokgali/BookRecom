using backend.services.gemini;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IGeminiClient _geminiClient;
        public UserController(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        [HttpGet]
        public async Task<IActionResult> UserTestThing(CancellationToken ct)
        {
            string generatedGeminiJson = await _geminiClient.GenerateContentAsync("what is dog", ct);

            return Ok(generatedGeminiJson);
        }
    }
}