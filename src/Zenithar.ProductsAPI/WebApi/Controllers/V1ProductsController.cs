using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zenithar.ProductsAPI.Application;
using Zenithar.ProductsAPI.WebApi.Dtos;

namespace Zenithar.ProductsAPI.WebApi.Controllers;

public sealed class V1ProductsController : V1ApiControllerBase
{
    private readonly ILogger<V1ProductsController> logger;
    private readonly IMapper mapper;
    private readonly IProductsService productsService;

    public V1ProductsController(
        ILogger<V1ProductsController> logger,
        IMapper mapper,
        IProductsService productsService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.productsService = productsService;
    }

    [HttpGet("products")]
    public async Task<ActionResult<V1ProductsList>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");

        var products = await productsService.Get(cancellationToken);

        var v1Products = mapper.Map<V1Product[]>(products);
        return new V1ProductsList(v1Products);
    }

    [HttpGet("products/{id}")]
    public async Task<ActionResult<V1Product>> Get(string id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product. ProductId: '{ProductId}'", id);

        var product = await productsService.Find(id, cancellationToken);

        if (product == null)
            return NotFound();

        return mapper.Map<V1Product>(product);
    }

    [HttpPost("products")]
    public async Task<ActionResult<V1Product>> Create(V1CreateProductRequest request,
                                                      CancellationToken cancellationToken)
    {
        var product = await productsService.Create(request.Name, request.Price, request.PreviewUrl, cancellationToken);

        return mapper.Map<V1Product>(product);
    }

    [HttpPut("products/{id}")]
    public async Task<ActionResult<V1Product>> Update(string id, V1UpdateProductRequest request,
                                                      CancellationToken cancellationToken)
    {
        try
        {
            var product =
                await productsService.Update(id, request.Name, request.Price, request.PreviewUrl, cancellationToken);

            return mapper.Map<V1Product>(product);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("products/{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        try
        {
            await productsService.Remove(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}