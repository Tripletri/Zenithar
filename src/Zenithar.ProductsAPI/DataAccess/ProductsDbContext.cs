using Microsoft.EntityFrameworkCore;
using Zenithar.ProductsAPI.DataAccess.Models;

namespace Zenithar.ProductsAPI.DataAccess;

internal sealed class ProductsDbContext : DbContext
{
    public DbSet<ProductModel> Products => Set<ProductModel>();

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
}