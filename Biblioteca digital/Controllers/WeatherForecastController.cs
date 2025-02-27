using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers;

[ApiController]
[Route("[controller]")]   // Define la ruta del controlador basada en el nombre de la clase
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]  // Define un endpoint GET con un nombre específico
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Genera una fecha futura

            TemperatureC = Random.Shared.Next(-20, 55),                // Genera una temperatura aleatoria en °C

            Summary = Summaries[Random.Shared.Next(Summaries.Length)]  // Asigna una descripción aleatoria
        })
        .ToArray();
    }
}
