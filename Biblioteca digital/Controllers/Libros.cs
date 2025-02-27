using Biblioteca_digital.Dtos;
using Biblioteca_digital.Dtos.books;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Biblioteca_digital.Controllers;

    [ApiController]
    [Route("/api/v1/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IServicioAutenticacion _authService;
        private readonly IReporitorioBase<Libro> _Librosrepositorio;
    private readonly IReporitorioBase<Prestamo> _PrestamosRepositorio;

        public BooksController(UserManager<Usuario> userManager,
            IServicioAutenticacion authService, IReporitorioBase<Libro> Librosrepositorio, IReporitorioBase<Prestamo> PrestamosRepositorio)
        {
            _userManager = userManager;
            _authService = authService;
           _Librosrepositorio = Librosrepositorio;
           _PrestamosRepositorio = PrestamosRepositorio;
        }

        [HttpPost("")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<dynamic>> Create(BooksCreatemodel request)
        {
        Libro libro = new Libro
        {
            Titulo = request.Titulo,
            Autor = request.Autor,
            Genero = request.Genero,
            ISBN = request.ISBN,
            Disponible = request.Estado
        };

        await _Librosrepositorio.CreateAsync(libro);

            return Ok(new { Respuesta= "Nuevo libro creado"});

        }

        [HttpGet("")]
        public async Task<ActionResult<dynamic>> ListAllBooks([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
        
           var listOfBooks = await _Librosrepositorio.GetPagedData(page, limit);

            return Ok(listOfBooks);

        }

    [HttpPost("loan")]
    public async Task<ActionResult<dynamic>> CreateLoan(CreateLoanModel request)
    {

        var libro = await _Librosrepositorio.GetByIdAsync( Guid.Parse(request.bookid));
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

    [HttpPost("loan/witdraw")]
    public async Task<ActionResult<dynamic>> CreateWithdraw(CreateWithdraw request)
    {

       
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var prestamo = await _PrestamosRepositorio.GetByIdAsync( Guid.Parse(request.PrestamoId));

        if (!prestamo.UsuarioId.Equals(userId))
        {
            return BadRequest(new { Error = $"Prestamo ${request.PrestamoId} no le pertenece" });
        }

        if (prestamo == null)
        {
            return BadRequest(new { Error = $"No existe el prestamo: {request.PrestamoId}" });
        }

        var libro = await _Librosrepositorio.GetByIdAsync( Guid.Parse(prestamo.LibroId));

        prestamo.Estado = Prestamo.EstadoPrestamo.DEVUELTO;

        libro.Disponible = true;


        await _PrestamosRepositorio.UpdateAsync(prestamo);
        await _Librosrepositorio.UpdateAsync(libro);


        await _PrestamosRepositorio.SaveChangesAsync();

        return Ok(new { Prestamo = prestamo });

    }

}



