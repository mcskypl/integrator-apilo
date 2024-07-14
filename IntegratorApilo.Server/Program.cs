global using IntegratorApilo.Server.Data;
global using IntegratorApilo.Server.Services.ApiloAuthorizationService;
global using IntegratorApilo.Server.Services.ApiloConnectionService;
global using IntegratorApilo.Server.Services.ApiloOrderService;
global using IntegratorApilo.Server.Services.ApiloWarehouseService;
global using IntegratorApilo.Server.Services.HostedService;
global using IntegratorApilo.Server.Services.OrderService;
global using IntegratorApilo.Server.Services.StockService;
global using IntegratorApilo.Shared;
global using IntegratorApilo.Shared.Apilo;
global using IntegratorApilo.Shared.Streamsoft;
global using Microsoft.EntityFrameworkCore;
global using RestSharp;
global using NLog;
global using NLog.Web;
using IntegratorApilo.Server;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Uruchamianie aplikacji");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddDbContext<SystemstDataContext>(options =>
{
    options.UseFirebird(builder.Configuration.GetConnectionString("SystemstConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<RestClient>(sp =>
{
    var client = new RestClient();
    client.AddDefaultHeader("Accept", "application/json");
    return client;
});
builder.Services.AddHttpClient();
//builder.Services.AddSingleton<RestClient>(sp => new RestClient());
builder.Services.AddScoped<IApiloAuthorizationService, ApiloAuthorizationService>();
builder.Services.AddScoped<IApiloOrderService, ApiloOrderService>();
builder.Services.AddScoped<IApiloWarehouseService, ApiloWarehouseService>();
builder.Services.AddScoped<IApiloConnectionService, ApiloConnectionService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddHostedService<OrderSyncService>();
builder.Services.AddHostedService<StockSyncService>();

var app = builder.Build();

app.MigrateDatabase<SystemstDataContext>();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
