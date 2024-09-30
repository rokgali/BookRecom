using System.Collections;
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
        public string? Description { get; set; }
        public List<string>? BookRecommendationIds { get; set; }
        public string? TakeawaysHeading {get;set;}
        public required Author Author {get;set;}
        public ICollection<Takeaway>? Takeaways {get;set;}
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}