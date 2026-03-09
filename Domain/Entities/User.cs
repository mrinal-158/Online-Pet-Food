
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string? Address { get; set; }
        public decimal? Otp { get; set; } = null;
        public DateTime OtpExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
        public bool IsVerified { get; set; } = false;
        public string? ProfilePicture { get; set; }
    }
}
