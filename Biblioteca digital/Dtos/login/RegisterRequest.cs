﻿namespace Biblioteca_digital.Dtos.login
{
    public class RegisterRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirm { get; set; }
    }
}
