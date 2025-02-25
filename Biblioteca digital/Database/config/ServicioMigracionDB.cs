using Microsoft.EntityFrameworkCore;

namespace Biblioteca_digital.Database.config
{

    public static class ServicioMigracionDB
    {
        public static async void DataBaseMigrationInitialization(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbCOntext = serviceScope.ServiceProvider.GetRequiredService<BibliotecaDbContext>();
                await dbCOntext.Database.MigrateAsync();


            }
        }
    }
}
