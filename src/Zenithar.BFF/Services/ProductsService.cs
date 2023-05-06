using Amazon.S3;
using Microsoft.Extensions.Options;
using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Configuration;
using Zenithar.BFF.Core;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.BFF.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly ILogger<ProductsService> logger;
    private readonly IOptions<ApplicationAWSOptions> options;
    private readonly IProductsClient client;
    private readonly IAmazonS3 s3;

    public ProductsService(ILogger<ProductsService> logger, IOptions<ApplicationAWSOptions> options,
                           IProductsClient client, IAmazonS3 s3)
    {
        this.logger = logger;
        this.options = options;
        this.client = client;
        this.s3 = s3;
    }

    public async Task<Product> Create(string name, int price, Stream image,
                                      CancellationToken cancellationToken = default)
    {
        var previewUrl = await UploadImage(image, cancellationToken);

        var request = new V1CreateProductRequest(name, price, previewUrl);

        logger.LogInformation("Creating product");
        var product = await client.Create(request, cancellationToken);
        logger.LogInformation("Successfully created product. '{ProductId}'", product.Id);

        return product;
    }

    private async Task<string> UploadImage(Stream image, CancellationToken cancellationToken)
    {
        var bucketName = options.Value.BucketName;
        var objectKey = Guid.NewGuid().ToString();

        logger.LogInformation("Uploading image. '{ObjectKey}'", objectKey);

        await s3.UploadObjectFromStreamAsync(bucketName, objectKey, image, new Dictionary<string, object>(),
            cancellationToken);

        logger.LogInformation("Successfully uploaded image. '{ObjectKey}'", objectKey);

        var previewUrl = $"https://storage.yandexcloud.net/zenithar/{objectKey}";
        return previewUrl;
    }

    public async Task<Product> Update(string id, string name, int price, Stream image,
                                      CancellationToken cancellationToken = default)
    {
        var previewUrl = await UploadImage(image, cancellationToken);

        var request = new V1UpdateProductRequest(name, price, previewUrl);

        logger.LogInformation("Updating product. '{ProductId}'", id);
        var product = await client.Update(id, request, cancellationToken);
        logger.LogInformation("Successfully updated product. '{ProductId}'", id);

        return product;
    }
}