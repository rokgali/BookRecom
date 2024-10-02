using backend.models.gemini.request;

namespace backend.services.gemini
{
    public interface IGeminiRequestFactory 
    {
        public GeminiRequest CreateRequest(string prompt);
    } 
}