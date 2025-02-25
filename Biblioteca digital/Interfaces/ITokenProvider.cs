using Biblioteca_digital.Model;

namespace Biblioteca_digital.Interfaces
{
    public interface IServicioToken
    {
        public Task<string> WriteToken(Usuario user);
    }
}
