namespace Biblioteca_digital.Model
{
    // Representa un préstamo de un libro en la biblioteca digital.
    public class Prestamo
    {
        // Identificador único del préstamo.
        public int Id { get; set; }

        // Identificador del libro prestado.
        public int LibroId { get; set; }

        // Libro asociado al préstamo.
        public Libro Libro { get; set; }

        // Identificador del usuario que realizó el préstamo.
        public int UsuarioId { get; set; }

        // Usuario que ha solicitado el préstamo.
        public Usuario Usuario { get; set; }

        // Fecha en la que se realizó el préstamo.
        // Se asigna automáticamente con la fecha y hora actual en UTC.
        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;

        // Fecha en la que el libro fue devuelto (puede ser nula si aún no ha sido devuelto).
        public DateTime? FechaDevolucion { get; set; }
    }

}
