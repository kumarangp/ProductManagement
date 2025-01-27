using AutoMapper;
using ProductManagement.Core.Entities;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Mappings {
    public class ProductMappingProfile : Profile {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            // Exclude CreatedAt when mapping from ProductDto to Product
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());  // Ignore CreatedAt
        }
    }
}
