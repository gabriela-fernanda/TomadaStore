using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services.v1;
using TomadaStore.SaleAPI.Services.v1.Interfaces;
using TomadaStore.SaleAPI.Services.v2;
using TomadaStore.SaleAPI.Services.v2.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddScoped<ISaleProducerService, SaleProducerService>();


builder.Services.AddHttpClient<ISaleService, SaleService>(client =>
        client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer"));

builder.Services.AddHttpClient<ISaleService, SaleService>(client =>
        client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product"));



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
