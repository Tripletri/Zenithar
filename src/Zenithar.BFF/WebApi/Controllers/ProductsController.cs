using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.WebApi.Dtos;

namespace Zenithar.BFF.WebApi.Controllers;

public class ProductsController : ApiControllerBase
{
    private readonly ILogger<ProductsController> logger;
    private readonly IMapper mapper;
    private readonly IProductsClient productsClient;

    public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductsClient productsClient)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.productsClient = productsClient;
    }

    [HttpGet("products")]
    public async Task<ActionResult<ProductsListDto>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");

        var products = await productsClient.GetAll(cancellationToken);

        var dtos = mapper.Map<ProductDto[]>(products);
        return new ProductsListDto(dtos);
    }
}