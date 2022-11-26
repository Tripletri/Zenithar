using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zenithar.BFF.Exceptions;
using Zenithar.BFF.WebApi.Dtos;

namespace Zenithar.BFF.WebApi.Filters;

internal sealed class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        logger.LogInformation("Handling exception. Exception: '{Exception}'", context.Exception);

        var result = MapResult(context.Exception);

        if (result == null)
        {
            result = new ObjectResult(new ErrorDto("server:unexpected", "Unexpected error occur")) {StatusCode = 500};
            logger.LogError(context.Exception, "Unmapped exception occur");
        }

        context.Result = result;
    }

    private ObjectResult? MapResult(Exception exception)
    {
        if (exception is NotFoundException notFoundException)
            return new ObjectResult(new ErrorDto("client:not-found", notFoundException.Message)) {StatusCode = 404};

        return null;
    }
}