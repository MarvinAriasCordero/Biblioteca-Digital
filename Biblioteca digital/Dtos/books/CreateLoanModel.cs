using System.ComponentModel.DataAnnotations;

namespace Biblioteca_digital.Dtos.books
{
    public record CreateLoanModel
    {
        [Required]
        public string bookid { get; set; } = string.Empty;  
        public string userId { get; set; } = string.Empty ;
        [Required]
        public DateOnly FechaReserva { get; set; }
        [Required]
        public  DateOnly FechaEntrega { get; set; }

    }
}
