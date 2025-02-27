using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_digital.Controllers
{

    // Filtro de autorización personalizado basado en roles.
    // Se asegura de que solo los usuarios con el rol especificado puedan acceder a ciertos recursos.

    public class RoleAuthFilter : AuthorizeAttribute, IAuthorizationFilter
        {

            private readonly string _role;


        // Constructor que recibe el rol requerido para acceder al recurso.

        public RoleAuthFilter(string role)
            {
                _role = role;
            }

        // Método que se ejecuta durante la autorización para validar si el usuario tiene el rol adecuado.
        public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;

            // Verifica si el usuario está autenticado
            if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;

                }

            // Obtiene los roles del usuario desde los claims
            var roles = user.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();

            // Verifica si el usuario tiene el rol requerido
            if (!roles.Contains(_role))
                {
                    context.Result = new UnauthorizedResult();
                }

            }
        }
}
