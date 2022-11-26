using Zenithar.BFF.Core;

namespace Zenithar.BFF.Services;

public interface IBasketService
{
    Task<Basket> AddProduct(string basketId, string productId, CancellationToken cancellationToken = default);
    Task<Basket> Get(string id, CancellationToken cancellationToken = default);
    Task<Basket> RemoveProduct(string basketId, string productId, CancellationToken cancellationToken = default);
    Task<Basket> Clear(string id, CancellationToken cancellationToken = default);
}