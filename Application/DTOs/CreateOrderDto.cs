using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record CreateOrderDto
    (
        string Name,
        string Phone,
        string Address,
        List<OrderItemDto> OrderItems
    );

    public record OrderItemDto
    (
        int ProductId,
        int Amount
    );
}
