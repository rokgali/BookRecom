namespace backend.models.gemini.request
{
    internal sealed class GeminiContent
    {
        public string Role { get; set; }
        public GeminiPart[] Parts { get; set; }
    }
}