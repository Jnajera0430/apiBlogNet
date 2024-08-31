using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenApi.Models;

namespace OpenApi.Services;

public class AuthServices : IAuthService
{
  private readonly IConfiguration _configuration;
  private readonly AppContext _appContext;
  private readonly PasswordHasher<User> _passwordHasher;

  private readonly ILogger<AuthServices> _logger;
  public AuthServices(AppContext appContext, IConfiguration configuration, ILogger<AuthServices> logger)
  {
    _appContext = appContext;
    _configuration = configuration;
    _passwordHasher = new PasswordHasher<User>();
    _logger = logger;
  }
  public AuthResponseDto Auth(AuthLoginDto credentials)
  {
    var foundUser = _appContext.users.FirstOrDefault(u => u.username == credentials.username);

    if (foundUser == null)
    {
      throw new UnauthorizedAccessException("User not found.");
    }
    _logger.LogDebug("esta es la contraseña del user: {password} y esta es credencial password {password1}", foundUser.password, credentials.password);
    var verificationResult = _passwordHasher.VerifyHashedPassword(foundUser, foundUser.password, credentials.password);
    if (verificationResult == PasswordVerificationResult.Failed)
    {
      throw new UnauthorizedAccessException("Invalid username or password.");
    }

    var token = GenerateJwtToken(foundUser.username);
    _logger.LogDebug("Este es el token: {token}", token);
    AuthResponseDto response = new()
    {
      token = token,
      message = "Authentication successful"
    };

    return response;
  }

  private string GenerateJwtToken(string username)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  string IAuthService.GenerateJwtToken(string username)
  {
    throw new NotImplementedException();
  }
}

public interface IAuthService
{
  public AuthResponseDto Auth(AuthLoginDto credentials);
  public string GenerateJwtToken(string username);
}


