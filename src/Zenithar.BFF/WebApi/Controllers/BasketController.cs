using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zenithar.BFF.Services;
using Zenithar.BFF.WebApi.Dtos;

namespace Zenithar.BFF.WebApi.Controllers;

public class BasketController : ApiControllerBase
{
    private readonly ILogger<BasketController> logger;
    private readonly IBasketService basketService;
    private readonly IMapper mapper;

    public BasketController(ILogger<BasketController> logger, IBasketService basketService, IMapper mapper)
    {
        this.logger = logger;
        this.basketService = basketService;
        this.mapper = mapper;
    }

    [HttpGet("basket")]
    public async Task<ActionResult<BasketDto>> Get(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting basket");

        var basketId = HttpContext.GetBasketId();

        var basket = await basketService.Get(basketId, cancellationToken);

        return mapper.Map<BasketDto>(basket);
    }

    [HttpPost("basket/products/{productId}")]
    public async Task<ActionResult<BasketDto>> AddProduct(string productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding product to basket");

        var basketId = HttpContext.GetBasketId();

        var basket = await basketService.AddProduct(basketId, productId, cancellationToken);

        return mapper.Map<BasketDto>(basket);
    }

    [HttpDelete("basket/products/{productId}")]
    public async Task<ActionResult<BasketDto>> RemoveProduct(string productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing product from basket");

        var basketId = HttpContext.GetBasketId();

        var basket = await basketService.RemoveProduct(basketId, productId, cancellationToken);

        return mapper.Map<BasketDto>(basket);
    }

    [HttpPost("basket/clear")]
    public async Task<ActionResult<BasketDto>> Clear(CancellationToken cancellationToken)
    {
        logger.LogInformation("Clearing basket");

        var basketId = HttpContext.GetBasketId();

        var basket = await basketService.Clear(basketId, cancellationToken);

        return mapper.Map<BasketDto>(basket);
    }
}