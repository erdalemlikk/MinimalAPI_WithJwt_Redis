namespace MinimalAPI.RedisCache;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync(string key, object data, TimeSpan timeSpan);
}
