using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zenithar.ProductsAPI.DataAccess.Models;
using Zenithar.ProductsAPI.Options;

namespace Zenithar.ProductsAPI.DataAccess;

internal sealed class DbStartUpService : IHostedService
{
    private readonly ILogger<DbStartUpService> logger;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly DbStartUpOptions options;

    public DbStartUpService(
        ILogger<DbStartUpService> logger,
        IServiceScopeFactory scopeFactory,
        IOptions<DbStartUpOptions> options)
    {
        this.logger = logger;
        this.scopeFactory = scopeFactory;
        this.options = options.Value;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

        logger.LogInformation("Migrating database");
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        if (options.Seed)
        {
            logger.LogInformation("Seeding is enabled");
            
            var productsExists = await dbContext.Products.AnyAsync(cancellationToken: cancellationToken);

            if (productsExists)
            {
                logger.LogInformation("Products already exists. Seeding will not be preformed");
                return;
            }
            
            logger.LogInformation("Seeding database with products");
            await dbContext.Products.AddRangeAsync(GenerateProducts(), cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static IEnumerable<ProductModel> GenerateProducts()
    {
        var fake = new Faker();

        var images = new[]
        {
            "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=640",
            "https://images.unsplash.com/photo-1572635196237-14b3f281503f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=640",
            "https://images.unsplash.com/photo-1485955900006-10f4d324d411?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=640",
        };

        for (var i = 0; i < 30; i++)
        {
            yield return new ProductModel(fake.Random.Guid().ToString(), fake.Commerce.ProductName(),
                fake.Random.Int(100, 999), fake.PickRandom(images));
        }
    }
}