namespace Zenithar.BFF.WebApi.Dtos;

public sealed record UpdateProductRequest(string Name, int Price, IFormFile? Image);