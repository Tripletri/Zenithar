namespace Zenithar.BFF.WebApi.Dtos;

public sealed record ProductsListDto(IReadOnlyCollection<ProductDto> Items);