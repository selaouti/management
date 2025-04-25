using Microsoft.EntityFrameworkCore;
using StockManagement.Infrastructure.Persistence;
using StockManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Ajoutez le serviceau conteneur DI
builder.Services.AddScoped<EntrepotService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<ArticleFournisseurService>();
builder.Services.AddScoped<CapteurService>();
builder.Services.AddScoped<UtilisateurService>();
builder.Services.AddScoped<LotService>();
builder.Services.AddScoped<MouvementStockService>();
builder.Services.AddScoped<HistoriqueTemperatureService>();
builder.Services.AddScoped<AlerteStockService>();
builder.Services.AddScoped<FournisseurService>();
builder.Services.AddScoped<StockService>();
// Configurez les options JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();