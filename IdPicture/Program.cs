using Microsoft.EntityFrameworkCore;
using MyId.Application.Interface;
using MyId.Application.Services;
using MyId.Persistence.Data;
using MyId.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with Azure SQL retry resilience
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)
    ));

// REGISTER YOUR SERVICES & REPOSITORIES HERE:
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepositories>();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in ALL environments (including Azure Production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyId API v1");
    c.RoutePrefix = "swagger";
});

// Redirect root URL (/) straight to Swagger UI (/swagger)
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();