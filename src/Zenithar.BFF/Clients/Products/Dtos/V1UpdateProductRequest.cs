namespace Zenithar.ProductsAPI.WebApi.Dtos;

public sealed record V1UpdateProductRequest(string Name, int Price, string PreviewUrl);