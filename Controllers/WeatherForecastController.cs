using Microsoft.AspNetCore.Mvc;

namespace proyectobasenet.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private static List<WeatherForecast> listWeatherForecast = new List<WeatherForecast>();

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
    if (listWeatherForecast == null || listWeatherForecast.Count == 0)
    {

      listWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToList();
    }
  }

  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    return listWeatherForecast;
  }
  [HttpPost]
  public IActionResult Post(WeatherForecast weatherForecast)
  {
    listWeatherForecast.Add(weatherForecast);
    return Ok();
  }
}
