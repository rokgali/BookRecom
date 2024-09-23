namespace backend.models.dto.Return 
{
    public record TakeawayDTO
    {
        public required string Name { get; set; }
        public required string Lesson { get; set; }
        public required string Episode { get; set; }
    }
}