using Biblioteca_digital.Dtos;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{
        [ApiController]
        [Route("/api/v1/[controller]")]
        [Authorize]
        public class librosController : ControllerBase
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IServicioAutenticacion _authService;
            private readonly IReporitorioBase<Libro> _Librosrepositorio;

            public librosController(UserManager<Usuario> userManager,
                IServicioAutenticacion authService, IReporitorioBase<Libro> Librosrepositorio)
            {
                _userManager = userManager;
                _authService = authService;
               _Librosrepositorio = Librosrepositorio;
            }

            [HttpPost("books")]
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

            [HttpGet("books")]
            public async Task<ActionResult<dynamic>> ListAllBooks()
            {


            var listOfBooks = await _Librosrepositorio.GetAllAsync();

                return Ok(listOfBooks);

            }

        }

    public record BooksCreatemodel
    {
        public string? Titulo { get; set; } = string.Empty;
        public string? Genero { get; set; } = string.Empty;
        public string? Autor { get; set; } = string.Empty;
        public string? ISBN { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;

    }
    }
