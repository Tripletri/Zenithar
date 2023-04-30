using System.Net;
using System.Web;
using AutoMapper;
using Microsoft.Extensions.Options;
using Zenithar.BFF.Clients.Products.Dtos;
using Zenithar.BFF.Core;
using Zenithar.BFF.Exceptions;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.BFF.Clients.Products;

internal sealed class ProductsClient : IProductsClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<ProductsClient> logger;
    private readonly IMapper mapper;
    private readonly ProductsClientOptions options;

    public ProductsClient(
        ILogger<ProductsClient> logger,
        IOptions<ProductsClientOptions> options,
        IMapper mapper,
        HttpClient httpClient)
    {
        this.logger = logger;
        this.options = options.Value;
        this.mapper = mapper;
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");

        var uri = new Uri(options.ApiUri, "api/v1/products");
        var response = await httpClient.GetFromJsonAsync<V1ProductsList>(uri, cancellationToken);

        if (response == null)
        {
            logger.LogWarning("Unable to get products");
            throw new ClientResponseResultException("Unable to get products");
        }

        return mapper.Map<Product[]>(response.Items);
    }

    public async Task<Product> Get(string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product. ProductId: '{ProductId}'", id);

        var uri = new Uri(options.ApiUri, $"api/v1/products/{HttpUtility.HtmlEncode(id)}");
        var response = await httpClient.GetAsync(uri, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new NotFoundException($"Product not found. ProductId: '{id}'. Details: '{response.ReasonPhrase}'");

        var dto = await response.Content.ReadFromJsonAsync<V1Product>(cancellationToken: cancellationToken);

        return mapper.Map<Product>(dto);
    }

    public async Task<Product> Create(V1CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var uri = new Uri(options.ApiUri, "api/v1/products");
        var response = await httpClient.PostAsJsonAsync(uri, request, cancellationToken);

        var dto = await response.Content.ReadFromJsonAsync<V1Product>(cancellationToken: cancellationToken);

        return mapper.Map<Product>(dto);
    }

    public async Task<Product> Update(string id, V1UpdateProductRequest request,
                                      CancellationToken cancellationToken = default)
    {
        var uri = new Uri(options.ApiUri, $"api/v1/products/{HttpUtility.HtmlEncode(id)}");
        var response = await httpClient.PutAsJsonAsync(uri, request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new NotFoundException($"Product not found. ProductId: '{id}'. Details: '{response.ReasonPhrase}'");

        var dto = await response.Content.ReadFromJsonAsync<V1Product>(cancellationToken: cancellationToken);

        return mapper.Map<Product>(dto);
    }

    public async Task Remove(string id, CancellationToken cancellationToken = default)
    {
        var uri = new Uri(options.ApiUri, $"api/v1/products/{HttpUtility.HtmlEncode(id)}");
        var response = await httpClient.DeleteAsync(uri, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new NotFoundException($"Product not found. ProductId: '{id}'. Details: '{response.ReasonPhrase}'");

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
    }
}