using AutoMapper;
using Zenithar.ProductsAPI.Core;
using Zenithar.ProductsAPI.DataAccess.Models;

namespace Zenithar.ProductsAPI.Application;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductModel, Product>();
    }
}