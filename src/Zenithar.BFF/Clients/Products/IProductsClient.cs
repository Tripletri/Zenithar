using Zenithar.BFF.Core;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.BFF.Clients.Products;

public interface IProductsClient
{
    Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);
    Task<Product> Get(string id, CancellationToken cancellationToken = default);
    Task<Product> Create(V1CreateProductRequest request, CancellationToken cancellationToken = default);
    Task<Product> Update(string id, V1UpdateProductRequest request, CancellationToken cancellationToken = default);
    Task Remove(string id, CancellationToken cancellationToken = default);
}