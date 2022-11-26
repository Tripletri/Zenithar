using AutoMapper;
using Zenithar.BFF.Clients.Products.Dtos;
using Zenithar.BFF.Core;

namespace Zenithar.BFF.Clients;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<V1Product, Product>();
    }
}