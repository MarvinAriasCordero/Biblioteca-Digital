using Biblioteca_digital.Dtos.login;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{
    [ApiController]  
    [Route("/api/v1/[controller]")]  // Define la ruta base del controlador como "/api/v1/authController"

    // Rutas de autorizacón......
    public class authController : ControllerBase
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IServicioAutenticacion _authService;

        // Constructor que recibe las dependencias mediante inyección de dependencias
        public authController(UserManager<Usuario> userManager,
                IServicioAutenticacion authService)
            {
                _userManager = userManager;
                _authService = authService;
            }


        // Endpoint para iniciar sesión
        [HttpPost("login")]
            public async Task<ActionResult<dynamic>> Login(LoginRequest loginRequest)
            {


                var result = await _authService.Login(loginRequest);

                return Ok(result);

            }

        // Endpoint para registrar un nuevo usuario
        [HttpPost("register")]
            public async Task<ActionResult<dynamic>> Register(RegisterRequest request)
            {


                var result = await _authService.Register(request);

                return Ok(result);

            }

        }
    }
