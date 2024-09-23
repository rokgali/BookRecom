using backend.models.database;

namespace backend.models.dto.Return 
{
    public record BookDescriptionAndTakeawaysDTO
    {
        public required string Description { get; set; }
        public required TakeawaysDTO Takeaways { get; set; }
    }
}