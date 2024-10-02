using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace backend.services.gemini
{
    internal sealed class GeminiClient : IGeminiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGeminiRequestFactory _geminiRequestFactory;

        public GeminiClient(HttpClient httpClient, ILogger<GeminiClient> logger, IConfiguration configuration, IGeminiRequestFactory geminiRequestFactory)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _geminiRequestFactory = geminiRequestFactory;
        }

        public async Task<string> GenerateContentAsync(string prompt, string model, CancellationToken cancellationToken)
        {
            var requestBody = _geminiRequestFactory.CreateRequest(prompt);
            var content = new StringContent(JsonSerializer.Serialize(requestBody, 
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}), Encoding.UTF8, "application/json");

            var apiKey = _configuration["Gemini:ApiKey"];
            var url = _configuration["Gemini:Url"];
            // gemini-1.5-flash-latest:generateContent
            // tunedModels/main-book-takeaways-5t3yvgirtmuh:generateContent

            var request = new HttpRequestMessage(HttpMethod.Post, url + model + "?key=" + apiKey)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }   
}