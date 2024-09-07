namespace backend.services.gemini
{
    public interface IGeminiClient
    {
        public Task<string> GenerateContentAsync(string prompt, CancellationToken cancellationToken);
    }
}