using System.ComponentModel.DataAnnotations;

namespace Biblioteca_digital.Dtos.books
{

    public record CreateWithdraw
    {
        [Required]
        public string PrestamoId { get; set; } = string.Empty;

    }
}
