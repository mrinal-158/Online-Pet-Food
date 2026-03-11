using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Online_Pet_Food.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(new { Message = "Product not found" });

            return Ok(product);
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            string? pictureUrl = null;

            if (createProductDto.Image != null && createProductDto.Image.Length > 0)
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
                var imagesDir = Path.Combine(webRoot, "images");
                Directory.CreateDirectory(imagesDir);

                var ext = Path.GetExtension(createProductDto.Image.FileName);
                var fileName = $"{Guid.NewGuid():N}{ext}";
                var filePath = Path.Combine(imagesDir, fileName);

                await using (var stream = System.IO.File.Create(filePath))
                {
                    await createProductDto.Image.CopyToAsync(stream);
                }

                pictureUrl = "/images/product" + fileName;
            }
            var product = await _productService.CreateProductAsync(createProductDto, pictureUrl);

            return Ok(product);
        }
    }
}
