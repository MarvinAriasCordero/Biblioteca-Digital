﻿namespace Biblioteca_digital.Model
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public bool Disponible { get; set; } = true;
    }

}
