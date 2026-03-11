using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public record CreateProductDto
    (
        string Name,
        string Category,
        string Consumers,
        string Description,
        decimal Price,
        int Stock,
        IFormFile? Image
    );
}
