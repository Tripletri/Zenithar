using Zenithar.BFF.Core;

namespace Zenithar.BFF.Clients.Products;

public interface IProductsClient
{
    Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);
    Task<Product> Get(string id, CancellationToken cancellationToken = default);
}