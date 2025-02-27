using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Biblioteca_digital.Model;
using Biblioteca_digital.Dtos.books;
using System.Security.Claims;
using Biblioteca_digital.Interfaces;
using Microsoft.Extensions.Logging;


namespace Biblioteca_digital.Controllers;


[ApiController] // Indica que esta clase es un controlador de API
[Route("api/v1/loans")] // Define la ruta del controlador basada en el nombre de la clase
public class Prestamos : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IServicioAutenticacion _authService;
    private readonly IReporitorioBase<Libro> _Librosrepositorio;
    private readonly IReporitorioBase<Prestamo> _PrestamosRepositorio;
    private readonly ILogger<Prestamos> _logger;

    public Prestamos(UserManager<Usuario> userManager,
        IServicioAutenticacion authService, IReporitorioBase<Libro> Librosrepositorio, IReporitorioBase<Prestamo> PrestamosRepositorio, ILogger<Prestamos> logger)
    {
        _userManager = userManager;
        _authService = authService;
        _Librosrepositorio = Librosrepositorio;
        _PrestamosRepositorio = PrestamosRepositorio;
        _logger = logger;// Inyección de dependencia para registrar eventos y errores
    }

    // Endpoint para registrar un préstamo de un libro
    [HttpPost("")]
    public async Task<ActionResult<dynamic>> CreateLoan(CreateLoanModel request)
    {
        try
        {
            var libro = await _Librosrepositorio.GetByIdAsync(Guid.Parse(request.bookid));
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!libro.Disponible)
            {
                return BadRequest(new { Error = $"Libro ${request.bookid} no esta disponible!" });
            }

            var prestamo = new Prestamo
            {
                UsuarioId = userId ?? "",
                LibroId = request.bookid,
                Libro = libro,
                FechaPrestamo = request.FechaReserva.ToDateTime(TimeOnly.MinValue),
                FechaDevolucion = request.FechaEntrega.ToDateTime(TimeOnly.MinValue),
                Estado = Prestamo.EstadoPrestamo.PRESTADO

            };

            await _PrestamosRepositorio.CreateAsync(prestamo);
            libro.Disponible = false;
            await _Librosrepositorio.UpdateAsync(libro);


            await _PrestamosRepositorio.SaveChangesAsync();

            return Ok(new { Prestamo = prestamo });

        }
        catch(Exception ex)
        {
            _logger.LogInformation("Hemos tenido un error procesando su solicitud {id}", ex.Message);
            return Problem("Hemos tenido un error procesando su solicitud");
        }
       

    }

    // Endpoint para registrar un préstamo de un libro
    [HttpPost("withdraw")]
    public async Task<ActionResult<dynamic>> CreateWithdraw(CreateWithdraw request)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prestamo = await _PrestamosRepositorio.GetByIdAsync(Guid.Parse(request.PrestamoId));

            // Si no existe el prestamo
            if (prestamo == null)
            {
                return BadRequest(new { Error = $"No existe el prestamo: {request.PrestamoId}" });
            }

            if(prestamo.Estado == Prestamo.EstadoPrestamo.DEVUELTO)
            {
                return BadRequest(new { Error = $"El prestamo: {request.PrestamoId} ya fue devuelto!" });
            }

            // Si el prestamo es de otro usuario
            if (!prestamo.UsuarioId.Equals(userId))
            {
                return BadRequest(new { Error = $"Prestamo ${request.PrestamoId} no le pertenece" });
            }


            var libro = await _Librosrepositorio.GetByIdAsync(Guid.Parse(prestamo.LibroId));

            prestamo.Estado = Prestamo.EstadoPrestamo.DEVUELTO;

            libro.Disponible = true;


            await _PrestamosRepositorio.UpdateAsync(prestamo);
            await _Librosrepositorio.UpdateAsync(libro);


            await _PrestamosRepositorio.SaveChangesAsync();

            return Ok(new { Prestamo = prestamo });

        }
        catch(Exception ex)
        {

            // Registra el error en los logs
            _logger.LogInformation("Hemos tenido un error procesando su solicitud {id}", ex.Message);

            return  Problem( "Hemos tenido un error procesando su solicitud" );
        }

      

    }

}


