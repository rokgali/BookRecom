namespace backend.models.gemini.request
{
    public sealed class GenerationConfig
    {
        public int Temperature { get; set; }
        public int TopK { get; set; }
        public int TopP { get; set; }
        public int MaxOutputTokens { get; set; }
        public List<object> StopSequences { get; set; }
    }
}