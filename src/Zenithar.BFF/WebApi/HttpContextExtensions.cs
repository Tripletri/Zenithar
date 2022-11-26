namespace Zenithar.BFF.WebApi;

internal static class HttpContextExtensions
{
    public static string GetBasketId(this HttpContext httpContext)
    {
        var id = httpContext.Items["basketId"] as string;
        
        if (id == null)
            throw new KeyNotFoundException("Unable to find BasketId in HttpContext items");

        return id;
    }
}