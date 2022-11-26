namespace Zenithar.ProductsAPI.Options;

internal sealed record DbStartUpOptions
{
    public bool Seed { get; set; }
}