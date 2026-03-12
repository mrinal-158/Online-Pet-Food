using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<string> CreateProductAsync(CreateProductDto createProductDto, string pictureUrl);
        Task<string> UpdateProductAsync(int id, CreateProductDto updateProductDto, string pictureUrl);
        Task<bool> DeleteProductAsync(int id);
    }
}
