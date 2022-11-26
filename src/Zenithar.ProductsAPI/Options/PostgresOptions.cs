namespace Zenithar.ProductsAPI.Options;

internal sealed record PostgresOptions
{
    public string ConnectionString { get; set; } = null!;
}