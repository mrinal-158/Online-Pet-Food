using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        public async Task<bool> CreateOrderAsync(int userId, CreateOrderDto createOrderDto)
        {
            //var user = await _userRepository.LoginUser(userId);
            //var order = new Order
            //{
            //    UserId = userId,
            //    Name = createOrderDto.Name,
            //    Phone = createOrderDto.Phone,
            //    Address = createOrderDto.Address,
            //    TotalPrice = createOrderDto.TotalPrice,
            //    OrderDate = createOrderDto.OrderDate,
            //    ExpectedDate = createOrderDto.ExpectedDate,
            //    OrderItems = createOrderDto.OrderItems.Select(oi => new OrderItem
            //    {
            //        OrderId = Order.Id,
            //        ProductId = oi.ProductId,
            //        Amount = oi.Amount,
            //        Price = oi.Price
            //    }).ToList()
            //};

            // Validate products and calculate prices
            var orderItems = new List<OrderItem>();
            decimal totalPrice = 0;

            foreach (var item in createOrderDto.OrderItems)
            {
                // ✅ Get product from database (don't trust client price!)
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);

                // ✅ Create OrderItem WITHOUT setting OrderId
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = product.Price * item.Amount  // ✅ Get price from database, not client
                };

                orderItems.Add(orderItem);
                totalPrice += product.Price * item.Amount;
            }

            // ✅ Create Order with OrderItems
            var order = new Order
            {
                UserId = userId,
                Name = createOrderDto.Name,
                Phone = createOrderDto.Phone,
                Address = createOrderDto.Address,
                OrderItems = orderItems,  // ✅ EF Core will automatically set OrderId
                TotalPrice = totalPrice,  // ✅ Server calculates, not client
                OrderDate = DateTime.UtcNow,  // ✅ Server sets, not client
                ExpectedDate = DateTime.UtcNow.AddDays(3)  // ✅ Server sets, not client
            };

            var orderResult = await _orderRepository.CreateOrderAsync(order);
            return true;
        }
    }
}
