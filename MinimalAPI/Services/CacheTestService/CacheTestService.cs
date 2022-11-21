namespace MinimalAPI.Services.CacheTestService;

public class CacheTestService : ICacheTestService
{
    #region Constructor
    private ICacheService _cacheService;
    public CacheTestService(ICacheService _cacheService)
        => this._cacheService = _cacheService;
    #endregion

    #region Methods
    public async Task<object> GetCacheModels()
    {
        CacheModel[] cacheModels = null;
        string? loadLocation = null;
        string DateNow = DateTime.Now.ToString("yyyy_MM_dd HH:mm");
        string cacheKey = $"CacheKey_{DateNow}";
        cacheModels = await _cacheService.GetAsync<CacheModel[]>(cacheKey);

        if (cacheModels is not null)
        {
            loadLocation = $"Loaded from the cache at {DateNow}";

            return new { loadLocation, cacheModels };
        }
        var cacheNames = new[]
        {
            "Testing1","Testing2","Testing3","Testing4"
        };
        cacheModels = Enumerable.Range(1, 4)
            .Select(countNumber => new CacheModel
            {
                Count = Random.Shared.Next(0, 10),
                CacheName = cacheNames[Random.Shared.Next(cacheNames.Length)]
            })
            .ToArray();
        loadLocation = $"Loaded from service at {DateNow}";

        await _cacheService.SetAsync(cacheKey, cacheModels, TimeSpan.FromMinutes(1));
        return new { loadLocation, cacheModels };
    }
    #endregion
}
