using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();

            var productDtos = products.Select(p => new ProductDto
            (
                Id: p.Id,
                Name: p.Name,
                Category: p.Category,
                Consumers: p.Consumers,
                Description: p.Description,
                Price: p.Price,
                Stock: p.Stock,
                ImageUrl: p.ImageUrl
            ));

            return productDtos;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            var productDto = new ProductDto
            (
                Id: product.Id,
                Name: product.Name,
                Category: product.Category,
                Consumers: product.Consumers,
                Description: product.Description,
                Price: product.Price,
                Stock: product.Stock,
                ImageUrl: product.ImageUrl
            );

            return productDto;
        }
        public async Task<string> CreateProductAsync(CreateProductDto createProductDto, string pictureUrl)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Category = createProductDto.Category,
                Consumers = createProductDto.Consumers,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                ImageUrl = pictureUrl
            };

            var createdProduct = await _productRepository.CreateProductAsync(product);
            if(createdProduct == null) return "Failed to create product";

            return "Product created successfully";
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ProductDto> UpdateProductAsync(int id, CreateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
