public interface IAiClientManager {
    public Task<string> GenerateContentAsync(AiClients clientType, string prompt, string model, CancellationToken ct);
}