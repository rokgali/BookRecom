using backend.models.gemini.request;

namespace backend.models.gemini.response
{
    internal sealed class Candidate {
        public required GeminiContent Content { get; set; }
    }
}
