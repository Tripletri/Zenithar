using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Zenithar.BFF.Core;
using Zenithar.BFF.Exceptions;

#pragma warning disable CS1998

namespace Zenithar.BFF.Repositories;

internal sealed class InMemoryBasketRepository : IBasketRepository
{
    private readonly Dictionary<string, Basket> basketsCollection = new();

    private readonly ILogger<InMemoryBasketRepository> logger;

    public InMemoryBasketRepository(ILogger<InMemoryBasketRepository> logger)
    {
        this.logger = logger;
    }

    public async Task<Basket?> Find(string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Finding basket. BasketId: '{BasketId}'", id);

        return basketsCollection.TryGetValue(id, out var basket) ? basket : null;
    }

    public async Task<Basket> Get(string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting basket. BasketId: '{BasketId}'", id);

        var basket = await Find(id, cancellationToken);

        if (basket == null)
            throw new NotFoundException("Basket not found");

        return basket;
    }

    public async Task<Basket> Create(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating basket");

        var basket = new Basket(Guid.NewGuid().ToString());

        basketsCollection.Add(basket.Id, basket);

        return basketsCollection[basket.Id];
    }

    public async Task<Basket> Save(Basket basket, CancellationToken cancellationToken)
    {
        logger.LogInformation("Saving basket. BasketId: '{BasketId}'", basket.Id);
        
        basketsCollection[basket.Id] = basket;

        return basketsCollection[basket.Id];
    }
}