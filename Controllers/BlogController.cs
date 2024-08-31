
using Microsoft.AspNetCore.Mvc;
using OpenApi.Models;
using OpenApi.Services;
namespace proyectobasenet.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogsController : ControllerBase
{
  private readonly ILogger<BlogsController> _logger;
  private readonly IBlogService _blogService;
  public BlogsController(ILogger<BlogsController> logger, IBlogService blogService)
  {
    _logger = logger;
    _blogService = blogService;
  }
  [HttpGet]
  public IActionResult Get()
  {
    _logger.LogDebug("saludos humanos");
    return Ok(_blogService.Get());
  }
  [HttpPost]
  public IActionResult Post([FromBody] CreateBlogDto blog)
  {
    _blogService.Save(blog);
    return Ok();
  }

  [HttpPut("{id}")]
  public IActionResult Put(string id, Blog blog)
  {
    _blogService.Update(id, blog);
    return Ok();
  }

  [HttpDelete]
  public IActionResult Delete(Guid id)
  {
    _blogService.Delete(id);
    return Ok();
  }
}