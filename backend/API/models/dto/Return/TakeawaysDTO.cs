namespace backend.models.dto.Return 
{
    public record TakeawaysDTO
    {
        public string? Heading { get; set; }
        public required List<TakeawayDTO> Takeaways { get; set; }
    }
}