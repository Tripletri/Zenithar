using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Core;
using Zenithar.BFF.Repositories;

namespace Zenithar.BFF.Services;

internal sealed class BasketService : IBasketService
{
    private readonly ILogger<BasketService> logger;
    private readonly IBasketRepository basketRepository;
    private readonly IProductsClient productsClient;

    public BasketService(
        ILogger<BasketService> logger,
        IBasketRepository basketRepository,
        IProductsClient productsClient)
    {
        this.logger = logger;
        this.basketRepository = basketRepository;
        this.productsClient = productsClient;
    }

    public async Task<Basket> Get(string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting basket. BasketId: '{BasketId}'", id);

        return await basketRepository.Get(id, cancellationToken);
    }

    public async Task<Basket> AddProduct(string basketId, string productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding product to basket. BasketId: '{BasketId}'. ProductId: '{ProductId}'",
            basketId, productId);

        var basket = await basketRepository.Get(basketId, cancellationToken);
        var product = await productsClient.Get(productId, cancellationToken);

        basket.AddProduct(product);

        return await basketRepository.Save(basket, cancellationToken);
    }

    public async Task<Basket> RemoveProduct(
        string basketId,
        string productId,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Removing product from basket. BasketId: '{BasketId}'. ProductId: '{ProductId}'",
            basketId, productId);

        var basket = await basketRepository.Get(basketId, cancellationToken);
        var product = await productsClient.Get(productId, cancellationToken);

        basket.RemoveProduct(product);

        return await basketRepository.Save(basket, cancellationToken);
    }

    public async Task<Basket> Clear(string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Clearing basket. BasketId: '{BasketId}'", id);

        var basket = await basketRepository.Get(id, cancellationToken);

        basket.Clear();

        return await basketRepository.Save(basket, cancellationToken);
    }
}