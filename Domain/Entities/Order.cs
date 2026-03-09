using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = 0;
        public string? Description { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpectedDate { get; set; } = DateTime.UtcNow.AddDays(3);
    }
}
