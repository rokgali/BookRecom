using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.database
{
    public class Takeaway 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Lesson { get; set; }
        public required string Episode { get; set; }
        public required Book Book {get;set;}
        [ForeignKey("FK_Book_Id")]
        public required int FK_Book_Id {get;set;}
    }
}