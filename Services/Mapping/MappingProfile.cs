using App.Repositories.Products;
using AppServices.Products;
using AppServices.Products.Create;
using AppServices.Products.Update;
using AutoMapper;

namespace AppServices.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ForMember(dect => dect.Name,
            opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<UpdateProductRequest, Product>().ForMember(dect => dect.Name,
            opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}

