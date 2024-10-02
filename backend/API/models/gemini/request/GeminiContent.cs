namespace backend.models.gemini.request
{
    public sealed class GeminiContent
    {
        public string? Role { get; set; }
        public required GeminiPart[] Parts { get; set; }
    }
}