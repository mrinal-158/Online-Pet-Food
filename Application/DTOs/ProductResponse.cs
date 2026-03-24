using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record ProductResponse
    (
        int Id,
        string Name,
        string Category,
        string Consumers,
        string Description,
        decimal Price,
        int Stock,
        string? ImageUrl
    );
}
