using backend.models.dto.ResponseArgs;

namespace backend.models.dto.TakeawayResponseDTO
{
    public record TakeawayResponseDTO(string heading, ICollection<TakeawayDTO> takeaways);
} 