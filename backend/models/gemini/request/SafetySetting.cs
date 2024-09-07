namespace backend.models.gemini.request
{
    internal sealed class SafetySetting
    {
        public string Category { get; set; }
        public string Threshold { get; set; }
    }
}