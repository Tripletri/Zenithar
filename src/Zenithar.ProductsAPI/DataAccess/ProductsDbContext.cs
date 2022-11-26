using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zenithar.ProductsAPI.DataAccess.Models;
using Zenithar.ProductsAPI.Options;

namespace Zenithar.ProductsAPI.DataAccess;

internal sealed class ProductsDbContext : DbContext
{
    private readonly PostgresOptions postgresOptions;

    public DbSet<ProductModel> Products => Set<ProductModel>();

    public ProductsDbContext(
        DbContextOptions<ProductsDbContext> options,
        IOptions<PostgresOptions> postgresOptions) : base(options)
    {
        this.postgresOptions = postgresOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(postgresOptions.ConnectionString);
    }
}