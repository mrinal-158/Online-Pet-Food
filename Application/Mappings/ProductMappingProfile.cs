using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Product, CreateProductDto>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
