using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Consumers { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
        public string? ImageUrl { get; set; }
    }
}
