using Microsoft.EntityFrameworkCore;
using StockManagement.Infrastructure.Persistence;
using StockManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<FournisseurService>();

// Ajouter les contrôleurs et autres services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurer Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();