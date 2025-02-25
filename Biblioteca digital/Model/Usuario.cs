using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_digital.Model
{
    public class Usuario : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Puede ser "Admin" o "User"
    }

}
