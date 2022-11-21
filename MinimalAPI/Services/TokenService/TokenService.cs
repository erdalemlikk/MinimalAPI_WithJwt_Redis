namespace MinimalAPI.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
	public TokenService(JwtSettings _jwtSettings)
        =>this._jwtSettings= _jwtSettings;

    public string GetToken(UserModel user)
    {
        if (ValidUser(user))
            return CreateToken();

        return "";
    }
    private bool ValidUser(UserModel user)
    {
        if (user.Username == "erdal" && user.Password == "erdal123")
            return true;

        return false;
    }
    private string CreateToken()
    {
        var issuer = _jwtSettings.Issuer;
        var audience = _jwtSettings.Audience;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
