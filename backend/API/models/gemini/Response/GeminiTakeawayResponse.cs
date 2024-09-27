namespace backend.models.gemini.response
{
    internal sealed class GeminiTakeawayResponse
    {
        public required Candidate[] Candidates  { get; set; }
    }
}