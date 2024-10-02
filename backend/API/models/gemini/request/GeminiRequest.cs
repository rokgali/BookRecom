namespace backend.models.gemini.request
{
    public class GeminiRequest
    {
        public required GeminiContent[] Contents { get; set; }
        public GenerationConfig? GenerationConfig { get; set; }
        public SafetySetting[]? SafetySettings { get; set; }
    }
}