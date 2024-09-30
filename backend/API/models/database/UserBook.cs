using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.database
{
    public enum BookStatus {
        InLibrary,
        InProgress,
        Finished
    }
    public class UserBook 
    {
        public int Id { get; set; }
        public required User User { get; set; }
        [ForeignKey("FK_User_Id")]
        public required int FK_User_Id {get;set;}
        public required Book Book { get; set; }
        [ForeignKey("FK_Book_Id")]
        public required int FK_Book_Id {get;set;}
        public BookStatus BookStatus { get; set; }
    }
}