using backend.services.gemini;

public enum AiClients {
    Gemini,
    OpenAI    
}

public class AiClientFactory {
    public HttpClient _httpClient {get;set;}
    public ILogger<IAiClient> _logger {get;set;}
    public IConfiguration _configuration {get;set;}
    public IGeminiRequestFactory _geminiRequestFactory {get;set;}

    public AiClientFactory(HttpClient httpClient, ILogger<IAiClient> logger, IConfiguration configuration, IGeminiRequestFactory geminiRequestFactory)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
        _geminiRequestFactory = geminiRequestFactory;
    }

    public IAiClient CreateAiClient(AiClients clientType)
    {
        return clientType switch
        {
            AiClients.Gemini => new GeminiClient(_httpClient, _logger, _configuration, _geminiRequestFactory),
            AiClients.OpenAI => new OpenAiClient(),
            _ => throw new NotImplementedException()
        };
    }
}