namespace backend.models.dto.get {
    public record BookDecriptionDTO 
    {
        public required string WorkId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
    }
}