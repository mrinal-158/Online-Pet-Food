using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var updateProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(updateProduct == null) return product;

            if(product.Name != null) updateProduct.Name = product.Name;
            if(product.Category != null) updateProduct.Category = product.Category;
            if(product.Consumers != null) updateProduct.Consumers = product.Consumers;
            if(product.Description != null) updateProduct.Description = product.Description;
            if(product.Price != null) updateProduct.Price = product.Price;
            if(product.Stock != null) updateProduct.Stock = product.Stock;
            if(product.ImageUrl != null) updateProduct.ImageUrl = product.ImageUrl;

            await _context.SaveChangesAsync();

            return updateProduct;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null) return false;

            await _context.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
