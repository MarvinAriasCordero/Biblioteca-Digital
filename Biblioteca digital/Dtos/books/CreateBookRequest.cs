namespace Biblioteca_digital.Dtos.books
{
    public record BooksCreatemodel
    {
        public string? Titulo { get; set; } = string.Empty;
        public string? Genero { get; set; } = string.Empty;
        public string? Autor { get; set; } = string.Empty;
        public string? ISBN { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;

    }
    
}
