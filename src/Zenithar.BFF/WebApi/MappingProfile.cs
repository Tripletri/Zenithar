using AutoMapper;
using Zenithar.BFF.Core;
using Zenithar.BFF.WebApi.Dtos;

namespace Zenithar.BFF.WebApi;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Basket, BasketDto>();
        CreateMap<ProductBatch, ProductsBatchDto>();
    }
}