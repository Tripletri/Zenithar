using Zenithar.ProductsAPI.Core;

namespace Zenithar.ProductsAPI.Application;

public interface IProductsService
{
    Task<IEnumerable<Product>> Get(CancellationToken cancellationToken = default);
    Task<Product?> Find(string id, CancellationToken cancellationToken = default);
}