using MyGeminiAPI.DependencyInjection;
using MyGeminiAPI.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddServices(builder)
        .AddSwagger()
        .AddControllers();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseGlobalErrorHandling();

    app.UseAllPipelines();

    app.MapControllers();

    app.Run();
}