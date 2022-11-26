using AutoMapper;
using Zenithar.ProductsAPI.Core;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.ProductsAPI.WebApi;

internal sealed class V1MappingProfile : Profile
{
    public V1MappingProfile()
    {
        CreateMap<Product, V1Product>();
    }
}