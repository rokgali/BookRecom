using System.Diagnostics.CodeAnalysis;
using backend.models.database;

namespace backend.models.dto.Create {
    public record CreateBookDTO {
        [DisallowNull]
        public required string Title {get;init;}
        [DisallowNull]
        public required string WorkId {get;init;}
        public DateTime DateAdded = DateTime.UtcNow;
        [DisallowNull]
        public int CoverId { get; init; }
        public string? TakeawaysHeading {get;set;}
        [DisallowNull]
        public required string AuthorKey {get;set;}
        [AllowNull]
        public string? Description { get; init; }
        [DisallowNull]
        public required ICollection<CreateTakeawayDTO> Takeaways {get;init;}
    }
}