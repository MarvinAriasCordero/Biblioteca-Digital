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
        [Authorize(Roles = "Admin")]
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

}



