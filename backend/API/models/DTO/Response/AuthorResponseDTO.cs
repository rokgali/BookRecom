namespace backend.models.dto.Response {
    public record AuthorResponseDTO
    {
        public required string Name { get; init; }
        public required string Key { get; init; }
    }
}
