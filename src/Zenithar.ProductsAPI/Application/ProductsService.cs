using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zenithar.ProductsAPI.Core;
using Zenithar.ProductsAPI.DataAccess;
using Zenithar.ProductsAPI.DataAccess.Models;

namespace Zenithar.ProductsAPI.Application;

internal sealed class ProductsService : IProductsService
{
    private readonly ILogger<ProductsService> logger;
    private readonly IMapper mapper;
    private readonly ProductsDbContext productsDb;

    public ProductsService(ILogger<ProductsService> logger, IMapper mapper, ProductsDbContext productsDb)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.productsDb = productsDb;
    }

    public async Task<IEnumerable<Product>> Get(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting products from database");

        var models = await productsDb.Products.ToArrayAsync(cancellationToken);

        logger.LogInformation("Get products count: {ProductsCount}", models.Length);

        return mapper.Map<Product[]>(models);
    }

    public async Task<Product?> Find(string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting product. ProductId: '{ProductId}'", id);

        var model = await productsDb.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            logger.LogInformation("Unable to find product");
            return null;
        }

        logger.LogInformation("Successfully found product");
        return mapper.Map<Product>(model);
    }

    public async Task<Product> Create(string name, int price, string previewUrl,
                                      CancellationToken cancellationToken = default)
    {
        var model = new ProductModel(Guid.NewGuid().ToString(), name, price, previewUrl);

        await productsDb.Products.AddAsync(model, cancellationToken);

        return mapper.Map<Product>(model);
    }

    public async Task<Product> Update(string id, string name, int price, string previewUrl,
                                      CancellationToken cancellationToken = default)
    {
        var model = await productsDb.Products.FindAsync(new { id }, cancellationToken);
        
        if (model == null)
            throw new KeyNotFoundException($"Product '{id}' not found");

        model.Name = name;
        model.Price = price;
        model.PreviewUrl = previewUrl;

        await productsDb.SaveChangesAsync(cancellationToken);

        return mapper.Map<Product>(model);
    }
}