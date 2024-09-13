using System.ComponentModel.DataAnnotations;

namespace backend.models.dto.create {
    public record RegisterDTO {
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [MaxLength(254)]
        public required string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(15)]
        public required string PhoneNumber {get;set;}
        public required string Password { get; set; }
    }
}