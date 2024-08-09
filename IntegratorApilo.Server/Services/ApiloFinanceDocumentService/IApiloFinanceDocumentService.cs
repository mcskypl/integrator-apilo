namespace IntegratorApilo.Server.Services.ApiloFinanceDocumentService;

public interface IApiloFinanceDocumentService
{
    Task<ServiceResponse<ApiloDocuments>> GetListOfAccountingDocuments(int idShop, int offset);
}
