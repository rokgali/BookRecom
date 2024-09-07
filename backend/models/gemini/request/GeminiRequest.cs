namespace backend.models.gemini.request
{
    internal sealed class GeminiRequest
    {
        public GeminiContent[] Contents { get; set; }
        public GenerationConfig GenerationConfig { get; set; }
        public SafetySetting[] SafetySettings { get; set; }
    }
}