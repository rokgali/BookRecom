using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        public DateTime DateRefreshed { get; set; }
        public int CoverId { get; set; }
        [MaxLength(20)]
        public string? AuthorKey { get; set; }
        public string? Description { get; set; }

        public Takeaways? TakeAways { get; set; }
        [ForeignKey("FK_TakeawaysId")]
        public int FK_TakeawaysId { get; set; }

        public List<string>? BookRecommendationIds { get; set; }
        public string? SaveError { get; set; }
        public IEnumerable<UserBook>? UserBooks { get; set; }
        public IEnumerable<BookChangeHistory>? BookChangeHistory { get; set; }
    }
}