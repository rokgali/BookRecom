namespace backend.models.database
{
    public class Takeaway 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Lesson { get; set; }
        public required string Episode { get; set; }
        public required Takeaways Takeaways { get; set; }
    }
}