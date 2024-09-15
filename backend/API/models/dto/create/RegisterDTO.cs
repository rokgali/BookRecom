using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace backend.models.dto.create {
    public record RegisterDTO {
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [MaxLength(254)]
        [DisallowNull]
        public required string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(15)]
        [DisallowNull]
        public required string PhoneNumber {get;set;}
        [DisallowNull]
        public required string Password { get; set; }
    }
}