namespace backend.models.dto.Create 
{
    public record CreateTakeawaysDTO(string? heading, ICollection<CreateTakeawayDTO> takeaways);
}