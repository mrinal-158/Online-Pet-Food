using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public record OrderResponse
    (
        int OrderId,
        string Name,
        string Phone,
        string? Email,
        string Address,
        string Description,
        List<OrderItemResponse> OrderItems,
        DateTime OrderDate,
        decimal TotalPrice,
        string PaymentStatus
    );

    public record OrderItemResponse
    (
        int ProductId,
        int Amount
    );
}
