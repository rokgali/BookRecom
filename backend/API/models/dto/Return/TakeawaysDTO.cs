namespace backend.models.dto.Return 
{
    public record TakeawaysDTO
    {
        public required string Heading { get; set; }
        public required List<TakeawayDTO> TakeAways { get; set; }
    }
}