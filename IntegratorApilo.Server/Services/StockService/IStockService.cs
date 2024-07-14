namespace IntegratorApilo.Server.Services.StockService;

public interface IStockService
{
    Task<ServiceResponse<bool>> Init();
    Task<ServiceResponse<List<Kartoteka>>> GetStocksFromDatabase(int idConfig, int idDatabase);
}
