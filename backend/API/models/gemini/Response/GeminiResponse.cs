namespace backend.models.gemini.response
{
    internal sealed class GeminiResponse
    {
        public required Candidate[] Candidates  { get; set; }
    }
}