using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public record CreateOrderDto
    (
        string Name,
        [Required]
        string Phone,
        string? Email,
        [Required]
        string Address,
        string Description,
        [Required]
        [MinLength(1, ErrorMessage = "At least one order item is required")]
        List<OrderItemDto> OrderItems
    );

    public record OrderItemDto
    (
        [Required]
        int ProductId,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        int Amount
    );
}
