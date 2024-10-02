namespace backend.models.gemini.request
{
    public sealed class SafetySetting
    {
        public string Category { get; set; }
        public string Threshold { get; set; }
    }
}