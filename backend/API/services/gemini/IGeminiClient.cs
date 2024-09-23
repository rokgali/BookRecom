namespace backend.services.gemini
{
    public interface IGeminiClient
    {
        public Task<string> GenerateContentAsync(string prompt, string model, CancellationToken cancellationToken);
    }
}