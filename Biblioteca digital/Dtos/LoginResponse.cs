namespace Biblioteca_digital.Dtos
{
    public class LoginResponse
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; } = string.Empty;
    }
}
