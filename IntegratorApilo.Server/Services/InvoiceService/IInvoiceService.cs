namespace IntegratorApilo.Server.Services.InvoiceService;

public interface IInvoiceService
{
    Task<ServiceResponse<bool>> Init();
}
