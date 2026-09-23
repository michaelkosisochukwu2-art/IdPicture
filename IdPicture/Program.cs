using Microsoft.EntityFrameworkCore;
using MyId.Application.Interface;
using MyId.Application.Services;
using MyId.Persistence.Data;
using MyId.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// REGISTER YOUR SERVICES & REPOSITORIES HERE:
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepositories>(); // Adjust GalleryRepository to match your class name

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();