using Microsoft.AspNetCore.RateLimiting;
using MyGeminiAPI.Configurations;
using MyGeminiAPI.Services;

namespace MyGeminiAPI.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

                // add as needed
                //context.ProblemDetails.Extensions["customError"] = new {Error = "Some other error", Details = new List<string>() { "error 1", "error 2"} };
            };
        });

        return services;
    }
        
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<GenAIConfig>(builder.Configuration.GetSection("GenAI"));

        services.AddHttpContextAccessor();

        services.AddHttpClient<IGeminiAPIService, GeminiAPIService>();

        services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            });
        });

        // Rate limiting
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            // all 4 types of rate limiting
            // Fixed Window
            rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
            {
                options.Window = TimeSpan.FromMinutes(5);
                options.PermitLimit = 10;
                options.QueueLimit = 3;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            });
        });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
