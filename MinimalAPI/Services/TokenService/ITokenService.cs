namespace MinimalAPI.Services.TokenService;

public interface ITokenService
{
    string GetToken(UserModel user);
}
