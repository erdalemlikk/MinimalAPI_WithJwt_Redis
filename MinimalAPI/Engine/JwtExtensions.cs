namespace MinimalAPI.Engine;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration builder)
    {
        JwtSettings jwtSettings = new();
        var section = builder.GetSection("JWT");
        section.Bind(jwtSettings);

        services.AddSingleton(jwtSettings);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(auth =>
        {
            auth.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey
                                    (Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
        services.AddAuthorization();
        return services;
    }
}
