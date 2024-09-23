using System.Diagnostics.CodeAnalysis;

namespace backend.models.dto.create {
    public record BookDTO {
        [DisallowNull]
        public required string Title {get;init;}
        [DisallowNull]
        public required string WorkId {get;init;}
        public DateTime DateAdded = DateTime.UtcNow;
        public int CoverId { get; init; }
        public int AuthorKey { get; set; }
        public string? AuthorName { get; set; }
        [DisallowNull]
        public required string Description { get; set; }
    }
}