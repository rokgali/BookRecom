namespace backend.models.gemini.response
{
    internal sealed class Takeaway 
    {
        public required string Name { get; set; }
        public required string Lesson { get; set; }
        public required string  Episode { get; set; }
    }
}