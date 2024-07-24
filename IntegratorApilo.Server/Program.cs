global using IntegratorApilo.Server.Data;
global using IntegratorApilo.Server.Services.AdminService;
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
global using NLog;
global using NLog.Web;
global using RestSharp;
using IntegratorApilo.Server;
using IntegratorApilo.Server.Services.ApiloFinanceDocumentService;
using IntegratorApilo.Server.Services.InvoiceService;

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

builder.Services.AddDbContext<MainDataContext>(options =>
{
    options.UseFirebird(builder.Configuration.GetConnectionString("SystemstConnection"));
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
builder.Services.AddScoped<IApiloFinanceDocumentService, ApiloFinanceDocumentService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddHostedService<OrderSyncService>();
builder.Services.AddHostedService<StockSyncService>();
builder.Services.AddHostedService<InvoiceHostedService>();

var app = builder.Build();

app.MigrateDatabase<MainDataContext>();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseWebAssemblyDebugging();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
