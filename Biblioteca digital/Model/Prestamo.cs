namespace Biblioteca_digital.Model
{
    // Representa un préstamo de un libro en la biblioteca digital.
    public class Prestamo
    {
      
        /// Identificador único del préstamo.
      
        public Guid Id { get; set; }

        // Identificador del libro prestado.
        public string LibroId { get; set; }

        
        /// Libro asociado al préstamo.
       
        public Libro? Libro { get; set; }

        
        /// Identificador del usuario que realizó el préstamo.
       
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
