using IntegratorApilo.Shared.Streamsoft;

namespace IntegratorApilo.Server.Services.AdminService;

public class AdminService : IAdminService
{
    private readonly SystemstDataContext _systemstDataContext;
    private readonly MainDataContext _mainDataContext;

    public AdminService(SystemstDataContext systemstDataContext, MainDataContext mainDataContext)
    {
        _systemstDataContext = systemstDataContext;
        _mainDataContext = mainDataContext;
    }

    public async Task<ServiceResponse<List<ApiloConfig>>> GetConfigs()
    {
        var result = new ServiceResponse<List<ApiloConfig>>
        {
            Data = new List<ApiloConfig>()
        };

        try
        {
            result.Data = await _systemstDataContext.ApiloConfig.Include(x => x.ApiloDatabases).ToListAsync();
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<List<ApiloShop>>> GetShops()
    {
        var result = new ServiceResponse<List<ApiloShop>>
        {
            Data = new List<ApiloShop>()
        };

        try
        {
            result.Data = await _mainDataContext.ApiloShop.Include(x => x.ApiloShopSettings)
                                                            .ThenInclude(c => c.ApiloSetting)
                                                          .Include(x => x.ApiloConnections)
                                                            .ThenInclude(x => x.ApiloAccounts)
                                                          .ToListAsync();
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<List<ApiloDatabase>>> UpdateConfig(ApiloConfig apiloConfigTmp)
    {
        var result = new ServiceResponse<List<ApiloDatabase>>
        {
            Data = new List<ApiloDatabase>()
        };

        try
        {
            var apiloConfig = await _systemstDataContext.ApiloConfig.Include(x => x.ApiloDatabases).FirstOrDefaultAsync(x => x.IdConfig == apiloConfigTmp.IdConfig);

            apiloConfig.AppName = apiloConfigTmp.AppName;
            apiloConfig.AppDescription = apiloConfigTmp.AppDescription;
            apiloConfig.IdMagazynStocks = apiloConfigTmp.IdMagazynStocks;
            apiloConfig.SyncOrdersMin = apiloConfigTmp.SyncOrdersMin;
            apiloConfig.ApiAddress = apiloConfigTmp.ApiAddress;

            foreach (var apiloDatabase in apiloConfig.ApiloDatabases)
            {
                apiloDatabase.SyncStocks = apiloConfigTmp.ApiloDatabases.FirstOrDefault(x => x.IdDatabase == apiloDatabase.IdDatabase).SyncStocks;
            }

            _systemstDataContext.ApiloConfig.Update(apiloConfig);
            await _systemstDataContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<List<ApiloShop>>> AddShop(ApiloShop apiloShop)
    {
        var result = new ServiceResponse<List<ApiloShop>>
        {
            Data = new List<ApiloShop>()
        };

        try
        {
            _mainDataContext.ApiloShop.Add(apiloShop);
            await _mainDataContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<List<ApiloShop>>> UpdateShop(ApiloShop apiloShop)
    {
        var result = new ServiceResponse<List<ApiloShop>>
        {
            Data = new List<ApiloShop>()
        };

        try
        {
            _mainDataContext.Entry(apiloShop).Property(x => x.ShopName).IsModified = true;
            _mainDataContext.Entry(apiloShop).Property(x => x.Description).IsModified = true;

            await _mainDataContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }
}
