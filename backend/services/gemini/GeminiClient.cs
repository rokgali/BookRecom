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

        public GeminiClient(HttpClient httpClient, ILogger<GeminiClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> GenerateContentAsync(string prompt, CancellationToken cancellationToken)
        {
            var requestBody = GeminiRequestFactory.CreateRequest(prompt);
            var content = new StringContent(JsonSerializer.Serialize(requestBody, 
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}), Encoding.UTF8, "application/json");

            var apiKey = _configuration["Gemini:ApiKey"];
            var url = _configuration["Gemini:Url"];

            var request = new HttpRequestMessage(HttpMethod.Post, url + "?key=" + apiKey)
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