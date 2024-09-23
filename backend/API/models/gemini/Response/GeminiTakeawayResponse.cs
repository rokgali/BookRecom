namespace backend.models.gemini.response
{
    internal sealed class GeminiTakeawayResponse
    {
        public required string Heading { get; set; }
        public required List<Takeaway> Takeaways  { get; set; }
    }
}