using Biblioteca_digital.Dtos;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{
        [ApiController]
        [Route("/api/v1/[controller]")]
        public class authController : ControllerBase
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IServicioAutenticacion _authService;

            public authController(UserManager<Usuario> userManager,
                IServicioAutenticacion authService)
            {
                _userManager = userManager;
                _authService = authService;
            }

            [HttpPost("login")]
            public async Task<ActionResult<dynamic>> Login(LoginRequest loginRequest)
            {


                var result = await _authService.Login(loginRequest);

                return Ok(result);

            }

            [HttpPost("register")]
            public async Task<ActionResult<dynamic>> Register(RegisterRequest request)
            {


                var result = await _authService.Register(request);

                return Ok(result);

            }

        }
    }
