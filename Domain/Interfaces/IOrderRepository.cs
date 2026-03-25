using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<string> CancelOrderAsync(int userId, int orderId);
        Task<string> CheckValidOrder(int userId, int orderId);
        Task<string> UpdatePaymentStatus(Order order);
    }
}
