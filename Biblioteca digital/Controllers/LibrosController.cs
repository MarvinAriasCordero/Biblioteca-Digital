using Biblioteca_digital.Dtos;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{

    /// Controlador para gestionar los libros en la biblioteca digital.
    /// Permite la creación y consulta de libros.
    [ApiController]
        [Route("/api/v1/[controller]")]
        [Authorize] // Requiere autenticación para acceder a los endpoints
    public class librosController : ControllerBase
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IServicioAutenticacion _authService;
            private readonly IReporitorioBase<Libro> _Librosrepositorio;

        // Constructor que inyecta las dependencias necesarias para la gestión de libros.
        public librosController(UserManager<Usuario> userManager,
                IServicioAutenticacion authService, IReporitorioBase<Libro> Librosrepositorio)
            {
                _userManager = userManager;
                _authService = authService;
               _Librosrepositorio = Librosrepositorio;
            }

        // Crea un nuevo libro en la biblioteca.
        /// Solo accesible para usuarios con rol "Admin".
        [HttpPost("books")]
            [Authorize(Roles ="Admin")] // Solo los administradores pueden crear libros
        public async Task<ActionResult<dynamic>> Create(BooksCreatemodel request)
            {

        // Se crea una nueva instancia del libro con los datos proporcionados.
            Libro libro = new Libro
            {
                Titulo = request.Titulo,
                Autor = request.Autor,
                Genero = request.Genero,
                ISBN = request.ISBN,
                Disponible = request.Estado
            };

            // Se guarda el libro en la base de datos

            await _Librosrepositorio.CreateAsync(libro);

                return Ok(new { Respuesta= "Nuevo libro creado"});

            }

        // Obtiene la lista de todos los libros disponibles en la biblioteca.

        [HttpGet("books")]
            public async Task<ActionResult<dynamic>> ListAllBooks()
            {


            var listOfBooks = await _Librosrepositorio.GetAllAsync();

                return Ok(listOfBooks);

            }

        }

    // Modelo de datos para la creación de un libro.
    public record BooksCreatemodel
    {
        public string? Titulo { get; set; } = string.Empty;
        public string? Genero { get; set; } = string.Empty;
        public string? Autor { get; set; } = string.Empty;
        public string? ISBN { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;

    }
    }
