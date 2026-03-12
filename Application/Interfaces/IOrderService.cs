using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(int userId, CreateOrderDto createOrderDto);
    }
}
