using System.ComponentModel.DataAnnotations;

namespace backend.models.database
{
    public class Book 
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public required string Title { get; set; }
        [MaxLength(8)]
        public required string WorkId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int CoverId { get; set; }
        [MaxLength(20)]
        public string? AuthorKey { get; set; }
        public string? Description { get; set; }
        public List<string>? MainIdeas { get; set; }
        public List<string>? BookRecommendationIds { get; set; }
        public IEnumerable<UserBook>? UserBooks { get; set; }
    }
}