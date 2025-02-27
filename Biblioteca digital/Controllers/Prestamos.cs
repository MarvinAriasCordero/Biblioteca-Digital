using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Biblioteca_digital.Model;


namespace Biblioteca_digital.Controllers;
{

[ApiController] // Indica que esta clase es un controlador de API
[Route("[controller]")] // Define la ruta del controlador basada en el nombre de la clase
public class Prestamos : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    }; // Lista de posibles descripciones del clima

    private readonly ILogger<WeatherForecastController> _logger;

    public Prestamos(ILogger<WeatherForecastController> logger)
    {
        _logger = logger; // Inyección de dependencia para registrar eventos y errores
    }

    [HttpGet(Name = "GetWeatherForecast")] // Define un endpoint GET con un nombre específico
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Genera una fecha futura
            TemperatureC = Random.Shared.Next(-20, 55), // Genera una temperatura aleatoria en °C
            Summary = Summaries[Random.Shared.Next(Summaries.Length)] // Asigna una descripción aleatoria
        })
        .ToArray(); // Convierte la colección en un array antes de devolverla
    }

    [HttpGet("RequestBook/{id}")] // Endpoint para solicitar un libro por ID
    public ActionResult<Book> RequestBook(int id)
    {
        var book = BookService.GetBookById(id); // Llamada al servicio para obtener el libro
        if (book == null)
        {
            return NotFound("Libro no encontrado"); // Devuelve un error 404 si no se encuentra el libro
        }
        return Ok(book); // Devuelve el libro encontrado
    }

    [HttpPost("SolicitarPrestamo")] // Endpoint para solicitar un préstamo de libro
    public ActionResult<Prestamo> SolicitarPrestamo([FromBody] Prestamo prestamo)
    {
        var nuevoPrestamo = PrestamoService.CrearPrestamo(prestamo); // Registra el préstamo en la base de datos
        if (nuevoPrestamo == null)
        {
            return BadRequest("No se pudo procesar el préstamo"); // Devuelve error si falla la solicitud
        }
        return Ok(nuevoPrestamo); // Devuelve el préstamo registrado con éxito
    }

}
}

