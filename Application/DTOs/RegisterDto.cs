using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record RegisterDto
    (
        [Required]
        string Name,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [Phone]
        string Phone,

        [Required]
        [MinLength(8)]
        string Password,

        string? Address,
        IFormFile? ProfilePicture
    );
}
