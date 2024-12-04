using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
var builder = WebApplication.CreateBuilder(args);

// Register Serilog with the Host.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration).
        WriteTo.Seq("http://localhost:5341");
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Restaurant API",
        Description = "API for managing restaurant-related operations."
    });
});

// mapster configuration
var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

var app = builder.Build();

app.UseErrorHandling();

app.UseSerilogRequestLogging();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseDeveloperExceptionPage();

    app.MapOpenApi();

    app.MapScalarApiReference(options => {
        options
        .WithTitle("Restaurant API")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
