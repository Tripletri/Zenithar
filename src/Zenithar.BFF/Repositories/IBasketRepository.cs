using Zenithar.BFF.Core;

namespace Zenithar.BFF.Repositories;

internal interface IBasketRepository
{
    Task<Basket?> Find(string id, CancellationToken cancellationToken = default);
    Task<Basket> Get(string id, CancellationToken cancellationToken = default);
    Task<Basket> Create(CancellationToken cancellationToken = default);
    Task<Basket> Save(Basket basket, CancellationToken cancellationToken = default);
}