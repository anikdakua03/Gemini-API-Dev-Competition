using Microsoft.AspNetCore.RateLimiting;
using MyGeminiAPI.Configurations;
using MyGeminiAPI.Services;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    // adding configurations
    builder.Services.Configure<GenAIConfig>(builder.Configuration.GetSection("GenAI"));

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddHttpClient<IGeminiAPIService, GeminiAPIService>();

    builder.Services.AddCors(options =>
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
    builder.Services.AddRateLimiter(rateLimiterOptions =>
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

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("Frontend");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseRateLimiter();

    app.MapControllers();

    app.Run();
}
