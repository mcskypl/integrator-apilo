using IntegratorApilo.Server.Services.ApiloOrderService;
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

    public ApiloController(IApiloAuthorizationService apiloAuthorizationService
                         , IApiloOrderService apiloOrderService
                         , IApiloWarehouseService apiloWarehouseService)
    {
        _apiloAuthorizationService = apiloAuthorizationService;
        _apiloOrderService = apiloOrderService;
        _apiloWarehouseService = apiloWarehouseService;
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
    public async Task<ActionResult<ServiceResponse<ApiloOrders>>> GetApiloOrders(ApiloConfig apiloConfig)
    {
        var result = await _apiloOrderService.GetSimpleListOfOrders(apiloConfig);
        return Ok(result);
    }

    [HttpGet]
    [Route("products")]
    public async Task<ActionResult<ServiceResponse<List<ApiloProduct>>>> GetApiloProducts(int apiloConfig)
    {
        var result = await _apiloWarehouseService.GetProductsList(apiloConfig);
        return Ok(result);
    }
}
