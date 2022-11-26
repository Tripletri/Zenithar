namespace Zenithar.BFF.WebApi.Dtos;

public sealed record BasketDto(IReadOnlyCollection<ProductsBatchDto> ProductBatches, double TotalPrice, int TotalCount);