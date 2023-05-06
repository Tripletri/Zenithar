using Zenithar.BFF.Core;

namespace Zenithar.BFF.Services;

public interface IProductsService
{
    Task<Product> Create(string name, int price, Stream image, CancellationToken cancellationToken = default);

    Task<Product> Update(string id, string name, int price, Stream image,
                         CancellationToken cancellationToken = default);
}