namespace MinimalAPI.Engine;

public static class RedisCacheExtensions
{
    public static IServiceCollection RegisterRedisCache(this IServiceCollection services, IConfiguration builder)
    {
        RedisCacheOptions redisCacheOptions = new();
        var section = builder.GetSection("Redis");
        section.Bind(redisCacheOptions);

        services.AddSingleton(redisCacheOptions);

        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = redisCacheOptions.Configuration;
            option.InstanceName = redisCacheOptions.InstanceName;
        });

        return services;
    }
}
