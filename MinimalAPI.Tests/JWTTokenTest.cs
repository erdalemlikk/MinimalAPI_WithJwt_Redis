using MinimalAPI.Engine;
using MinimalAPI.Model;
using MinimalAPI.Services.TokenService;
using Moq;
using NUnit.Framework;

namespace MinimalAPI.Tests;

public class Tests
{
    private Mock<JwtSettings> _jwtSettings;
    private TokenService _testClass;

    private readonly UserModel wrongUser = new()
    {
        Username = "wrongUsername",
        Password = "WrongPassword"
    };

    private readonly UserModel validUser = new()
    {
        Username = "erdal",
        Password = "erdal123"
    };
    
    [SetUp]
    public void Setup()
    {
        _jwtSettings = new Mock<JwtSettings>();
        _jwtSettings.Object.Audience = "github/erdalemlik";
        _jwtSettings.Object.Issuer = "github/erdalemlik";
        _jwtSettings.Object.Key = "Key for github/erdalemlik";
        _testClass = new TokenService(_jwtSettings.Object);
    }

    [Test]
    public void Can_ValidUser_With_True_Credentials()
    {
        bool tokenResult =_testClass.ValidUser(validUser);
        Assert.IsTrue(tokenResult);
    }

    [Test]
    public void CanNot_ValidateUser_With_Wrong_Credentials()
    {
        bool tokenResult = _testClass.ValidUser(wrongUser);
        Assert.IsFalse(tokenResult);
    }

    [Test]
    public void Can_Give_Token_To_ValidUser()
    {
        string token = _testClass.GetToken(validUser);
        Assert.AreNotEqual("",token);
    }

    [Test]
    public void CanNot_Give_Token_To_InvalidUser()
    {
        string token = _testClass.GetToken(wrongUser);
        Assert.IsEmpty(token);
    }
}