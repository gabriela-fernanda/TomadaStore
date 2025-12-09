using TomadaStore.SaleConsumer.Data;
using TomadaStore.SaleConsumer.Repositories;
using TomadaStore.SaleConsumer.Repositories.Interfaces;
using TomadaStore.SaleConsumer.Services;
using TomadaStore.SaleConsumer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<ISaleConsumerRepository, SaleConsumerRepository>();
builder.Services.AddSingleton<ISaleConsumerService, SaleConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
