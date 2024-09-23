using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.database
{
    public class Takeaways
    {
        public int Id { get; set; }
        public required string Heading { get; set; }
        public required List<Takeaway> TakeAways { get; set; }

        public required Book Book { get; set; }
        [ForeignKey("FK_BookId")]
        public int FK_BookId { get; set; }
    }
}