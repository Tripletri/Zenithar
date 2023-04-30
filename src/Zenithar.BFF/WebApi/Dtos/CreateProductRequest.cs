namespace Zenithar.BFF.WebApi.Dtos;

public sealed record CreateProductRequest(string Name, int Price, IFormFile Image);