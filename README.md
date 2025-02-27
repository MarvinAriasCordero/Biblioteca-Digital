# Biblioteca-Digital

## Principales funcionalidades:
### Autenticacion
- Login  api/v1/auth/login
- Registro api/v1/auth/register
### Books
- Lista de Libros api/v1/books?page=1&limit=10
- Crear un Libro  api/v1/books
- Presta un Libro api/v1/loan
- Devolver un libro api/v1/loan/withdraw

 ###  Instalación

 ### Uso de SQL Server como base de datos 
 El projecto usa una base de datos en Memoria. Para usar SQL Server 
 
indicarlo en el contenedor de dependencias en la clase `Program.cs`

 ```csharp
 
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
  options.UseSqlServer(connectionString));

 ```


 
 Si se va a ejecutar en modo debug puede descomentar la función 
 para efectuar una migracion de la base de datos usando Code-first.

 ```csharp
 if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
    ServicioMigracionData.InicializarMigracionDeDatos(app);
}
 ```
De lo contrario debe efectuar las migraciones antes de probar la aplicación
para efectuar las migraciones primero debe descargar el componente ef-tool
```
dotnet tool update --global dotnet-ef
```

luego ejecutar en la carpeta del projecto `Biblioteca digital/ `
```
dotnet restore
dotnet tool restore
dotnet ef database update 
````

 ## despliege usando docker y docker-compose

 Se dedea installar la aplicacion, una opcion muy util tanto para debug como para 
 despliegue local es usar ` Docker`, tanto en Windows como en Linux. 

 para desplegar la aplicacion en un contenedor de docker-compose
 asi como una base de datos SQL Server. 

pasos para el depliegue: 
crear un archivo `.env` en la raiz del projecto : 

```
DB_PASSWD={tu_Contraseña_SQLServer}
```

Reemplazar los valores para `tu_Contraseña_SQLServer` y  `${DB_PASSWD}` con sus credenciales y guardarlos de manera segura. 

Ubicarse en la carpeta raiz del projecto y correr el projecto usando docker-compose

```
 docker-compose build
 docker-compose up
```



#### Tecnologias : 
 C# / .NET8
 SQL Server/SQLlite
 Entity Framework Core
 Identity
 Docker / Docker compose (para pruebas y despliegue local)
