﻿using Biblioteca_digital.Dtos.login;

namespace Biblioteca_digital.Interfaces
{
    public interface IServicioAutenticacion
    {
            Task<dynamic> Register(RegisterRequest registroRequest);

            Task<dynamic> Login(LoginRequest loginRequest);


        // Task<dynamic> CambiarContraseña(CambioDeContraseñaRequestDto request);
    }
}
