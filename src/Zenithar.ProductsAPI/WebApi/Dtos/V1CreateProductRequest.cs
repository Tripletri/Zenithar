namespace Zenithar.ProductsAPI.WebApi.Dtos;

public sealed record V1CreateProductRequest(string Name, int Price, string PreviewUrl);