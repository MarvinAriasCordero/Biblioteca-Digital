namespace Biblioteca_digital.Model
{
  //  Representa un libro dentro del sistema de la biblioteca digital.
    public class Libro
    {
        // Identificador único del libro.
        public Guid Id { get; set; }

        // Título del libro.
        public string? Titulo { get; set; } = string.Empty;

        // Nombre del autor del libro.
        public string? Autor { get; set; } = string.Empty;

        // Género literario del libro.
        public string? Genero { get; set; } = string.Empty;

        // Código ISBN del libro.
        public string? ISBN { get; set; } = string.Empty;

        // Indica si el libro está disponible para préstamo.
        public bool Disponible { get; set; } = false;
    }

}
