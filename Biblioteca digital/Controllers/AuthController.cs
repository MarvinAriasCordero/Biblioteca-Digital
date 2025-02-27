using Biblioteca_digital.Dtos;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{

    // Controlador para la autenticación de usuarios.
    // Maneja el inicio de sesión y el registro de nuevos usuarios.

    [ApiController]
        [Route("/api/v1/[controller]")]
        public class authController : ControllerBase
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IServicioAutenticacion _authService;

        // Constructor que inyecta las dependencias necesarias para la autenticación.

        public authController(UserManager<Usuario> userManager,
                IServicioAutenticacion authService)
            {
                _userManager = userManager;
                _authService = authService;
            }

        // Endpoint para el inicio de sesión de un usuario.
        // <returns>Token de autenticación si el inicio de sesión es exitoso.

        [HttpPost("login")]
            public async Task<ActionResult<dynamic>> Login(LoginRequest loginRequest)
            {


                var result = await _authService.Login(loginRequest);

                return Ok(result);

            }

        // Endpoint para registrar un nuevo usuario.
        // <returns>Información del usuario registrado.

        [HttpPost("register")]
            public async Task<ActionResult<dynamic>> Register(RegisterRequest request)
            {


                var result = await _authService.Register(request);

                return Ok(result);

            }

        }
    }
