using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();

            var productDtos = products.Select(p => new ProductResponse
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
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            var productDto = new ProductResponse
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
        public async Task<CreateProductDto> CreateProductAsync(CreateProductDto createProductDto, string pictureUrl)
        {
            //var product = new Product
            //{
            //    Name = createProductDto.Name,
            //    Category = createProductDto.Category,
            //    Consumers = createProductDto.Consumers,
            //    Description = createProductDto.Description,
            //    Price = createProductDto.Price,
            //    Stock = createProductDto.Stock,
            //    ImageUrl = pictureUrl
            //};

            var product = _mapper.Map<Product>(createProductDto);
            product.ImageUrl = pictureUrl;

            var createdProduct = await _productRepository.CreateProductAsync(product);

            if (createdProduct == null) return null;

            var createdProductDto = _mapper.Map<CreateProductDto>(createdProduct);

            return createdProductDto;
        }
        public async Task<string> UpdateProductAsync(int id, CreateProductDto updateProductDto, string pictureUrl)
        {
            var product = new Product
            {
                Name = updateProductDto.Name,
                Category = updateProductDto.Category,
                Consumers = updateProductDto.Consumers,
                Description = updateProductDto.Description,
                Price = updateProductDto.Price,
                Stock = updateProductDto.Stock,
                ImageUrl = pictureUrl
            };

            var updatedProduct = await _productRepository.UpdateProductAsync(id, product);
            if (updatedProduct == null) return "Failed to update product";

            return "Product updated successfully";
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);

            return result;
        }
    }
}
