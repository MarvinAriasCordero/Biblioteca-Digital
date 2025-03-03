﻿using Biblioteca_digital.Dtos.login;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca_digital.Servicios
{
    // Clase que implementa el servicio de autenticación
    public class ServicioAutenticacion : IServicioAutenticacion
    {

        // Dependencias necesarias para la autenticación y gestión de usuarios
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IServicioToken _tokenProvider;

        // Constructor que recibe las dependencias mediante inyección de dependencias
        public ServicioAutenticacion(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager, IServicioToken tokenProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;

        }

        // Método que maneja el inicio de sesión
        public async Task<dynamic> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

                if (result.Succeeded)
                {
                    return new
                    {
                        UserDetails = new
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                        },
                        Token = await _tokenProvider.WriteToken(user)

                    };

                }
                else return new
                {
                    Mesage = "Login atemp faild!"
                };
            }
            else return new
            {
                Error = "User not found!"
            };

        }

        // Método para registrar un nuevo usuario
        public async Task<dynamic> Register(RegisterRequest registroRequest)
        {

            var user = new Usuario
            {
                UserName = registroRequest.Name,
                Email = registroRequest.Email,
               

            };

            var reuslt = await _userManager.CreateAsync(user, registroRequest.Password);

            if (reuslt.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return new
                {
                    Usernam = user.UserName,
                    Email = user.Email,
                    Message = "User careated!"
                };
            }
            else return new
            {
                Errors = reuslt.Errors
            };
        }
    }
}
