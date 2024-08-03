using Microsoft.AspNetCore.Diagnostics;
using MyGeminiAPI.Errors;

namespace MyGeminiAPI.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception is null)
            {
                return Results.Problem();
            }

            // custom global exception handling           

            return exception switch
            {
                ServiceException serviceException => Results.Problem(statusCode : serviceException.StatusCode, detail: serviceException.ErrorMessage),
                _ => Results.Problem()
            };
        });

        return app;
    }
    public static WebApplication UseAllPipelines(this WebApplication app)
    {
        app.UseCors("Frontend");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRateLimiter();

        return app;
    }
}
