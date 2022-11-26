using Microsoft.AspNetCore.Mvc.Filters;
using Zenithar.BFF.Repositories;

namespace Zenithar.BFF.WebApi.Filters;

internal sealed class BasketContextActionFilter : IAsyncActionFilter
{
    private readonly ILogger<BasketContextActionFilter> logger;
    private readonly IBasketRepository basketRepository;

    public BasketContextActionFilter(ILogger<BasketContextActionFilter> logger, IBasketRepository basketRepository)
    {
        this.logger = logger;
        this.basketRepository = basketRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        logger.LogInformation("Getting basketId from cookies");

        var basketId = context.HttpContext.Request.Cookies["basketId"];

        if (basketId != null)
        {
            var basket = await basketRepository.Find(basketId);
            basketId = basket != null ? basket.Id : null;
        }

        if (basketId == null)
        {
            var basket = await basketRepository.Create();
            basketId = basket.Id;
        }

        context.HttpContext.Items["basketId"] = basketId;

        await next();

        context.HttpContext.Response.Cookies.Append("basketId", basketId);
    }
}