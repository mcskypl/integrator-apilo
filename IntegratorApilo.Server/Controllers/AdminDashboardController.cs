using Microsoft.AspNetCore.Mvc;

namespace IntegratorApilo.Server.Controllers
{
    [Route("api/admin-dashboard")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminDashboardController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("configs")]
        public async Task<ActionResult<ServiceResponse<ApiloConfig>>> GetConfigs()
        {
            var result = await _adminService.GetConfigs();
            return Ok(result);
        }

        [HttpPatch]
        [Route("config")]
        public async Task<ActionResult<ServiceResponse<ApiloConfig>>> UpdateConfig(ApiloConfig apiloConfig)
        {
            var result = await _adminService.UpdateConfig(apiloConfig);
            return Ok(result);
        }

        [HttpGet]
        [Route("shops")]
        public async Task<ActionResult<ServiceResponse<ApiloShop>>> GetShops()
        {
            var result = await _adminService.GetShops();
            return Ok(result);
        }

        [HttpPost]
        [Route("shop")]
        public async Task<ActionResult<ServiceResponse<ApiloShop>>> AddShop(ApiloShop apiloShop)
        {
            var result = await _adminService.AddShop(apiloShop);
            return Ok(result);
        }

        [HttpPatch]
        [Route("shop")]
        public async Task<ActionResult<ServiceResponse<ApiloShop>>> UpdateConfig(ApiloShop apiloShop)
        {
            var result = await _adminService.UpdateShop(apiloShop);
            return Ok(result);
        }
    }
}
