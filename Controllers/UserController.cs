
using Microsoft.AspNetCore.Mvc;
using OpenApi.Models;
using OpenApi.Services;
namespace proyectobasenet.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;
  private readonly IUserServices _userServices;
  public UserController(ILogger<UserController> logger, IUserServices userServices)
  {
    _logger = logger;
    _userServices = userServices;
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Ok(_userServices.Get());
  }

  [HttpPost]
  public IActionResult Post([FromBody] CreateUserDto user)
  {
    _userServices.Save(user);
    return Ok();
  }
}