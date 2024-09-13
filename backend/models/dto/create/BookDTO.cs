namespace backend.models.dto.create {
    public record BookDTO {
        public required string Title {get;init;}
        public required string WorkId {get;init;}
        public DateTime DateAdded = DateTime.UtcNow;
        public int CoverId { get; init; }
        public int AuthorKey { get; set; }
        public required string Description { get; set; }
    }
}