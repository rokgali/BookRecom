using System.Diagnostics.CodeAnalysis;
using backend.models.database;
using backend.models.dto.ResponseArgs;

namespace backend.models.dto.RequestArgs {
    public record BookDTO {
        [DisallowNull]
        public required string Title {get;init;}
        [DisallowNull]
        public required string WorkId {get;init;}
        public DateTime DateAdded = DateTime.UtcNow;
        [DisallowNull]
        public int CoverId { get; init; }
        [DisallowNull]
        public AuthorDTO? Author {get;init;}
        [AllowNull]
        public string? Description { get; init; }
        [AllowNull]
        public TakeawaysDTO? Takeaways {get;init;}
    }
}