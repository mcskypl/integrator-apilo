using IntegratorApilo.Server.Services.ApiloFinanceDocumentService;
using IntegratorApilo.Server.Services.ApiloOrderService;
using IntegratorApilo.Server.Services.InvoiceService;
using IntegratorApilo.Shared.Streamsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static IntegratorApilo.Shared.Apilo.ApiloProducts;

namespace IntegratorApilo.Server.Controllers;

[Route("api/apilo")]
[ApiController]
public class ApiloController : ControllerBase
{
    private readonly IApiloAuthorizationService _apiloAuthorizationService;
    private readonly IApiloOrderService _apiloOrderService;
    private readonly IApiloWarehouseService _apiloWarehouseService;
    private readonly IApiloFinanceDocumentService _apiloFinanceDocumentService;
    private readonly IInvoiceService _invoiceService;

    public ApiloController(IApiloAuthorizationService apiloAuthorizationService
                         , IApiloOrderService apiloOrderService
                         , IApiloWarehouseService apiloWarehouseService
                         , IApiloFinanceDocumentService apiloFinanceDocumentService
                         , IInvoiceService invoiceService)
    {
        _apiloAuthorizationService = apiloAuthorizationService;
        _apiloOrderService = apiloOrderService;
        _apiloWarehouseService = apiloWarehouseService;
        _apiloFinanceDocumentService = apiloFinanceDocumentService;
        _invoiceService = invoiceService;
    }

    [HttpGet]
    [Route("tokens")]
    public async Task<ActionResult<ServiceResponse<ApiloTokens>>> GetApiloTokens()
    {
        var result = await _apiloAuthorizationService.GetTokens();
        return Ok(result);
    }

    [HttpGet]
    [Route("orders")]
    public async Task<ActionResult<ServiceResponse<ApiloShop>>> GetApiloOrders(int idShop)
    {
        var result = await _apiloOrderService.GetSimpleListOfOrders(idShop);
        return Ok(result);
    }

    [HttpGet]
    [Route("status")]
    public async Task<ActionResult<ServiceResponse<List<ApiloOrderStatus>>>> GetApiloStatus(int idShop)
    {
        var result = await _apiloOrderService.GetOrderStatus(idShop);
        return Ok(result);
    }

    [HttpGet]
    [Route("products")]
    public async Task<ActionResult<ServiceResponse<List<ApiloProduct>>>> GetApiloProducts(int apiloConfig)
    {
        var result = await _apiloWarehouseService.GetProductsList(apiloConfig);
        return Ok(result);
    }

    [HttpGet]
    [Route("order/{idApiloOrder}")]
    public async Task<ActionResult<ServiceResponse<List<ApiloProduct>>>> GetApiloProducts(int idShop, string idApiloOrder)
    {
        var result = await _apiloOrderService.GetOrderDetails(idShop, idApiloOrder);
        return Ok(result);
    }

    [HttpGet]
    [Route("finance-documents")]
    public async Task<ActionResult<ServiceResponse<List<ApiloProduct>>>> GetFinanceDocuments(int idShop)
    {
        var result = await _apiloFinanceDocumentService.GetListOfAccountingDocuments(idShop);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("finance-documents")]
    public async Task<ActionResult<ServiceResponse<List<ApiloProduct>>>> AddFinanceDocuments()
    {
        var result = await _invoiceService.Init();
        return Ok(result);
    }
}
