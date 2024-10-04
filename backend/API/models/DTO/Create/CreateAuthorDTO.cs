using System.Diagnostics.CodeAnalysis;

namespace backend.models.dto.Create 
{  
    public record CreateAuthorDTO
    {
        public required string Name { get; set; }
        public required string Key { get; set; }
    }
}