var builder = WebApplication.CreateBuilder(args);
/*  Add Swagger     */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICacheService,CacheService>();
builder.Services.AddSingleton<ICacheTestService,CacheTestService>();
builder.Services.AddSingleton<ITokenService,TokenService>();

builder.Services.RegisterRedisCache(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

/* Minimal Token API */
app.MapPost("/getToken", 
    [AllowAnonymous](UserModel user,ITokenService tokenService) =>
{
    string token =  tokenService.GetToken(user);
    if (token == "")
        return Results.Unauthorized();

    return Results.Ok(token);

});

/* Minimal GET API */
app.MapGet("/testingRedisCache",
    [Authorize](ICacheTestService _cacheTestService) =>
{
    return _cacheTestService.GetCacheModels();
});
/*              */
app.Run();
