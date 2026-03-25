using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(int userId, CreateOrderDto createOrderDto);
        Task<List<OrderResponse>> GetOrdersByUserIdAsync(int userId);
        Task<string> CancelOrderAsync(int userId, int orderId);
        Task<string> PayOrderAsync(int userId, int orderId);
    }
}
