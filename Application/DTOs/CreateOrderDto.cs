using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public record CreateOrderDto
    (
        string Name,
        string Phone,
        string? Email,
        string Address,
        string Description,
        List<OrderItemDto> OrderItemsDto
    );

    public record OrderItemDto
    (
        int ProductId,
        int Amount
    );
}
