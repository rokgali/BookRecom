using System.Diagnostics.CodeAnalysis;

namespace backend.models.dto.RequestArgs 
{  
    public record AuthorDTO
    {
        public required string Name { get; set; }
        public required string Key { get; set; }
    }
}