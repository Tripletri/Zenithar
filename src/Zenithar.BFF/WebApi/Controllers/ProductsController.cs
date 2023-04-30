using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Services;
using Zenithar.BFF.WebApi.Dtos;

namespace Zenithar.BFF.WebApi.Controllers;

public class ProductsController : ApiControllerBase
{
    private readonly ILogger<ProductsController> logger;
    private readonly IMapper mapper;
    private readonly IProductsClient productsClient;
    private readonly IProductsService productsService;

    public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductsClient productsClient,
                              IProductsService productsService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.productsClient = productsClient;
        this.productsService = productsService;
    }

    [HttpGet("products")]
    public async Task<ActionResult<ProductsListDto>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");

        var products = await productsClient.GetAll(cancellationToken);

        var dtos = mapper.Map<ProductDto[]>(products);
        return new ProductsListDto(dtos);
    }

    [HttpPost("products")]
    public async Task<ActionResult<ProductDto>> Create([FromForm] CreateProductRequest request,
                                                       CancellationToken cancellationToken)
    {
        var product = await productsService.Create(request.Name, request.Price, request.Image.OpenReadStream(),
            cancellationToken);

        return mapper.Map<ProductDto>(product);
    }

    [HttpPut("products/{id}")]
    public async Task<ActionResult<ProductDto>> Update(string id, [FromForm] UpdateProductRequest request,
                                                       CancellationToken cancellationToken)
    {
        var product = await productsService.Update(id, request.Name, request.Price, request.Image?.OpenReadStream(),
            cancellationToken);

        return mapper.Map<ProductDto>(product);
    }

    [HttpDelete("products/{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        await productsClient.Remove(id, cancellationToken);
        return NoContent();
    }
}