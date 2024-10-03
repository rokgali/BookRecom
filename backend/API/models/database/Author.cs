using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using backend.models.gemini.validationAttributes;

namespace backend.models.database
{
    public class Author 
    {
        public int Id {get;set;}
        [MaxLength(100)]
        public required string Name {get;set;}
        [MaxLength(11)]
        public required string Key {get;set;}
        public ICollection<Book>? Books {get;set;} 
    }
}