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
        Task<ProductDto> UpdateProductAsync(int id, CreateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
