using System.ComponentModel.DataAnnotations;

namespace Biblioteca_digital.Dtos.books
{

    public record CreateWithdraw
    {
        [Required]
        public string bookid { get; set; } = string.Empty;
        [Required]
        public string userId { get; set; } = string.Empty;
        [Required]
        public string PrestamoId { get; set; } = string.Empty;

    }
}
