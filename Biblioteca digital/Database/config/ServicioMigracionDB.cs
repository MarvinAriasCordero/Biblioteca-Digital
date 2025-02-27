using Microsoft.EntityFrameworkCore;

namespace Biblioteca_digital.Database.config
{

    public static class ServicioMigracionDB
    {
        public static async void DataBaseMigrationInitialization(IApplicationBuilder app)
        {
            // Se crea un ámbito de servicio para obtener instancias de los servicios registrados.

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Se obtiene una instancia de 'BibliotecaDbContext' desde los servicios de la aplicación.
                var dbCOntext = serviceScope.ServiceProvider.GetRequiredService<BibliotecaDbContext>();

                // Se obtiene una instancia de 'BibliotecaDbContext' desde los servicios de la aplicación.
                await dbCOntext.Database.MigrateAsync();


            }
        }
    }
}
