namespace backend.services.gemini
{
    public interface IAiClient
    {
        public Task<string> GenerateContentAsync(string prompt, string model, CancellationToken cancellationToken);
    }
}