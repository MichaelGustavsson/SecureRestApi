using Microsoft.EntityFrameworkCore;
using productsApi.Data;
using productsApi.Middleware;
using productsApi.Utilities;
using productsApi.Utilities.Filters;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Add services to the container.
// =====================================================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

// Add dependency injection...
builder.Services.AddTransient<IApiKeyValidator, ApiKeyValidator>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddCors();

builder.Services.AddControllers();

var app = builder.Build();

// =====================================================
// Pipeline...
// =====================================================
app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

// =====================================================
// Seed data...
// =====================================================
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    await InitializeDb.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Det gick fel vid migrering av databasen.");
}

app.Run();
