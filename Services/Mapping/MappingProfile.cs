using App.Repositories.Products;
using AppServices.Products;
using AutoMapper;

namespace AppServices.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}

