using backend.services.gemini;

public class OpenAiClient : IAiClient {
    public OpenAiClient()
    {

    }

    public async Task<string> GenerateContentAsync(string prompt, string model, CancellationToken cancellationToken)
    {
        return "Generated thing";
    }
}