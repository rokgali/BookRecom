namespace backend.models.dto.ResponseArgs 
{
    public record TakeawaysDTO(string? heading, ICollection<TakeawayDTO> takeaways);
}