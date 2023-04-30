using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Core;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.BFF.Services;

public interface IProductsService
{
    Task<Product> Create(string name, int price, Stream image, CancellationToken cancellationToken = default);

    Task<Product> Update(string id, string name, int price, Stream? image,
                         CancellationToken cancellationToken = default);
}

internal sealed class ProductsService : IProductsService
{
    private readonly IProductsClient client;

    public ProductsService(IProductsClient client)
    {
        this.client = client;
    }

    public async Task<Product> Create(string name, int price, Stream image,
                                      CancellationToken cancellationToken = default)
    {
        //TODO upload image
        var previewUrl =
            "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=640";

        var request = new V1CreateProductRequest(name, price, previewUrl);

        return await client.Create(request, cancellationToken);
    }

    public async Task<Product> Update(string id, string name, int price, Stream? image,
                                      CancellationToken cancellationToken = default)
    {
        //TODO upload image
        var previewUrl =
            "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=640";

        var request = new V1UpdateProductRequest(name, price, previewUrl);

        return await client.Update(id, request, cancellationToken);
    }
}