
using Microsoft.AspNetCore.Mvc;
using OpenApi.Models;
using OpenApi.Services;
namespace proyectobasenet.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
  private readonly ILogger<AuthController> _logger;
  private readonly IAuthService _authServices;

  public AuthController(ILogger<AuthController> logger, IAuthService authServices)
  {
    _logger = logger;
    _authServices = authServices;
  }

  [HttpPost]
  [Route("login")]
  public IActionResult Login([FromBody] AuthLoginDto credentials)
  {
    try
    {
      _logger.LogDebug($"User '{credentials.username}' username.");
      return Ok(_authServices.Auth(credentials));
    }
    catch (UnauthorizedAccessException ex)
    {
      _logger.LogWarning($"Authentication failed for user '{credentials.username}': {ex.Message}");
      return Unauthorized(new { message = ex.Message });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"An unexpected error occurred while authenticating user '{credentials.username}'.");
      return StatusCode(500, new { message = "An unexpected error occurred." });
    }
  }
}