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
            // Create OrderItems from DTO and calculate total price
            var orderItems = new List<OrderItem>();
            decimal totalPrice = 0;

            foreach (var item in createOrderDto.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = product.Price * item.Amount
                };
                orderItems.Add(orderItem);
                totalPrice += product.Price * item.Amount;
            }

            var order = new Order
            {
                UserId = userId,
                Name = createOrderDto.Name,
                Phone = createOrderDto.Phone,
                Email = createOrderDto.Email,
                Address = createOrderDto.Address,
                Description = createOrderDto.Description,
                OrderItems = orderItems,
                TotalPrice = totalPrice,
                OrderDate = DateTime.UtcNow,
                ExpectedDate = DateTime.UtcNow.AddDays(3)
            };

            var orderResult = await _orderRepository.CreateOrderAsync(order);
            return true;
        }
        public async Task<List<OrderResponse>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            var orderResponse = new List<OrderResponse>();
            foreach (var item in orders)
            {
                var orderItems = new List<OrderItemResponse>();
                foreach (var orderItem in item.OrderItems)
                {
                    orderItems.Add(new OrderItemResponse
                    (
                        ProductId: orderItem.ProductId,
                        Amount: orderItem.Amount
                    ));
                }
                orderResponse.Add(new OrderResponse
                (
                    OrderId: item.Id,
                    Name: item.Name,
                    Phone: item.Phone,
                    Email: item.Email,
                    Address: item.Address,
                    Description: item.Description,
                    OrderItems: orderItems,
                    OrderDate: item.OrderDate,
                    TotalPrice: item.TotalPrice,
                    PaymentStatus: item.PaymentStatus.ToString()
                ));
            }

            return orderResponse;
        }
        public async Task<string> CancelOrderAsync(int userId, int orderId)
        {
            var result = await _orderRepository.CancelOrderAsync(userId, orderId);

            return result;
        }
        public async Task<string> PayOrderAsync(int userId, int orderId)
        {
            var result = await _orderRepository.CheckValidOrder(userId, orderId);
            if(result != "Valid") return result;

            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            
            var payment = await _orderRepository.UpdatePaymentStatus(order);
            return "Success";
        }
        
    }
}
