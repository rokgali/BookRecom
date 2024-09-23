using System.ComponentModel.DataAnnotations;
using backend.models.dto.create;
using backend.models.gemini.validationAttributes;

namespace backend.models.dto.get {
    public record GeminiBookDataDTO 
    {
        [GreaterThanZeroAndNotMoreThan(5)]
        public int? NumberOfTakeaways { get; set; }
        public required BookDTO bookDTO { get; set; }
    }
}