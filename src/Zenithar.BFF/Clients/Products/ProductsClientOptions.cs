namespace Zenithar.BFF.Clients.Products;

public sealed record ProductsClientOptions
{
    public Uri ApiUri { get; set; } = null!;
};