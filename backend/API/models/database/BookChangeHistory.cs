namespace backend.models.database 
{
    public class BookChangeHistory
    {
        public int Id { get; set; }
        public DateTime InsertionDate { get; set; }
        public IEnumerable<Book>? Book { get; set; }
    }
}