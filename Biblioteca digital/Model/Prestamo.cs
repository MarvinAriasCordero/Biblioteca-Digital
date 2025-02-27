namespace Biblioteca_digital.Model
{
    // Representa un préstamo de un libro en la biblioteca digital.
    public class Prestamo
    {
        /// <summary>
        /// Identificador único del préstamo.
        /// </summary>
        public Guid Id { get; set; }

        // Identificador del libro prestado.
        public string LibroId { get; set; }

        /// <summary>
        /// Libro asociado al préstamo.
        /// </summary>
        public Libro? Libro { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el préstamo.
        /// </summary>
        public string UsuarioId { get; set; }

        ///// <summary>
        ///// Usuario que ha solicitado el préstamo.
        ///// </summary>
        //public Usuario? Usuario { get; set; }        ///// <summary>
        ///// Usuario que ha solicitado el préstamo.
        ///// </summary>
        //public Usuario? Usuario { get; set; }

        /// <summary>
        /// Fecha en la que se realizó el préstamo.
        /// Se asigna automáticamente con la fecha y hora actual en UTC.
        /// </summary>
        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha en la que el libro fue devuelto (puede ser nula si aún no ha sido devuelto).
        /// </summary>
        public DateTime? FechaDevolucion { get; set; }

        /// <summary>
        /// Estado del prestamo
        /// </summary>
        public EstadoPrestamo Estado { get; set; } = EstadoPrestamo.PRESTADO;

        public enum EstadoPrestamo
        {
            PRESTADO = 1,
            DEVUELTO = 2
        }

    }



}
