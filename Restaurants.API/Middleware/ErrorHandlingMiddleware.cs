﻿
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);

            logger.LogWarning(notFound.Message);
        }
        catch (ForbidException forbid)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbid.Message);

            logger.LogWarning(forbid.Message);
        }
        catch (NotFoundUserContext notFoundUser)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundUser.Message);

            logger.LogWarning(notFoundUser.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}