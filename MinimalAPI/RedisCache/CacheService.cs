namespace MinimalAPI.RedisCache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
	public CacheService(IDistributedCache _distributedCache)
        => this._distributedCache = _distributedCache;
 
    public async Task<T?> GetAsync<T>(string key)
    {
        string? value = await _distributedCache.GetStringAsync(key);

        return (value == null) ? default : JsonConvert.DeserializeObject<T>(value);

    }
    public async Task SetAsync(string key, object data, TimeSpan timeSpan)
    {
        var option = new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan);
        option.AbsoluteExpirationRelativeToNow = timeSpan;
        var value = JsonConvert.SerializeObject(data);
        await _distributedCache.SetStringAsync(key, value, option);
    }
}
