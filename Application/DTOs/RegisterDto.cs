using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record RegisterDto
    (
        string Name,
        string Email,
        string Phone,
        string Password,
        string? Address,
        IFormFile? ProfilePicture
    );
}
