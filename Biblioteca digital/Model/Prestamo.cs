namespace Biblioteca_digital.Model
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;
        public DateTime? FechaDevolucion { get; set; }
    }

}
