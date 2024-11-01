using backend.services.gemini;

public class AiClientManager : IAiClientManager {
    public AiClientFactory _aiClientFactory {get;set;}
    public AiClientManager(AiClientFactory aiClientFactory) {
        _aiClientFactory = aiClientFactory;
    }

    public async Task<string> GenerateContentAsync(AiClients clientType, string prompt, string model, CancellationToken ct)
    {
        return await _aiClientFactory.CreateAiClient(clientType).GenerateContentAsync(prompt, model, ct);
    }
}