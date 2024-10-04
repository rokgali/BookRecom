using System.Diagnostics.CodeAnalysis;
using backend.models.dto.Response;

namespace backend.models.dto.Response
{
    public record BookResponseDTO {
        [DisallowNull]
        public required string Title {get;init;}
        [DisallowNull]
        public required string WorkId {get;init;}
        public DateTime DateAdded = DateTime.UtcNow;
        [DisallowNull]
        public int CoverId { get; init; }
        public string? TakeawaysHeading {get;set;}
        [DisallowNull]
        public AuthorResponseDTO Author {get;init;}
        [AllowNull]
        public string? Description { get; init; }
        [DisallowNull]
        public required ICollection<TakeawayResponseDTO> Takeaways {get;init;}
    }
}