using Zenithar.ProductsAPI.Core;

namespace Zenithar.ProductsAPI.Application;

public interface IProductsService
{
    Task<IEnumerable<Product>> Get(CancellationToken cancellationToken = default);
    Task<Product?> Find(string id, CancellationToken cancellationToken = default);
    Task<Product> Create(string name, int price, string previewUrl, CancellationToken cancellationToken = default);

    Task<Product> Update(string id, string name, int price, string previewUrl,
                         CancellationToken cancellationToken = default);

    Task Remove(string id, CancellationToken cancellationToken);
}