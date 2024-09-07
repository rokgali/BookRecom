namespace backend.models.database
{
    public enum BookStatus {
        Recommended,
        InProgress,
        Finished
    }
    public class UserBook 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int BookId { get; set; }
        public required Book Book { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}