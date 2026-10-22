# Biblioteca Digital

API REST para la gestión de una biblioteca digital, desarrollada con C# y .NET 8. Permite registrar usuarios e iniciar sesión, administrar libros y controlar los préstamos y devoluciones.

<!-- Agrega aquí una captura de la API en funcionamiento (Swagger, Postman o similar) y borra este comentario.
Ejemplo: ![Captura de la API](docs/captura.png) -->

## Tecnologías

- C# / .NET 8
- Entity Framework Core (migraciones code-first)
- Identity (autenticación de usuarios)
- SQL Server / SQLite (por defecto usa una base de datos en memoria)
- Docker y Docker Compose (para pruebas y despliegue local)

## Funcionalidades principales

### Autenticación

| Endpoint | Descripción |
|---|---|
| `api/v1/auth/login` | Inicio de sesión |
| `api/v1/auth/register` | Registro de usuarios |

### Libros y préstamos

| Endpoint | Descripción |
|---|---|
| `api/v1/books?page=1&limit=10` | Lista de libros con paginación |
| `api/v1/books` | Crear un libro |
| `api/v1/loan` | Prestar un libro |
| `api/v1/loan/withdraw` | Devolver un libro |

## Instalación

Primero, clona el repositorio:

```bash
git clone https://github.com/MarvinAriasCordero/Biblioteca-Digital.git
cd Biblioteca-Digital
```

Por defecto el proyecto usa una **base de datos en memoria**, por lo que no necesita configuración adicional para probarlo. Para usar SQL Server hay dos opciones:

### Opción 1: Docker y Docker Compose

Es la opción más práctica, tanto para depuración como para despliegue local, y funciona en Windows y en Linux. Levanta la aplicación junto con una base de datos SQL Server.

1. Crea un archivo `.env` en la raíz del proyecto con la contraseña de SQL Server:

   ```
   DB_PASSWD={tu_Contraseña_SQLServer}
   ```

   Reemplaza `{tu_Contraseña_SQLServer}` por tu contraseña y guárdala de forma segura. No subas el archivo `.env` al repositorio.

2. Desde la raíz del proyecto, ejecuta:

   ```bash
   docker-compose build
   docker-compose up
   ```

### Opción 2: SQL Server instalado localmente

1. En la clase `Program.cs`, indica el uso de SQL Server en el contenedor de dependencias:

   ```csharp
   builder.Services.AddDbContext<BibliotecaDbContext>(options =>
       options.UseSqlServer(connectionString));
   ```

2. Aplica las migraciones de la base de datos de una de estas dos formas:

   **En modo debug:** descomenta la función que ejecuta la migración automática (code-first) al iniciar:

   ```csharp
   if (app.Environment.IsDevelopment())
   {
       app.UseMigrationsEndPoint();

       ServicioMigracionData.InicializarMigracionDeDatos(app);
   }
   ```

   **De forma manual:** antes de probar la aplicación, instala la herramienta `ef-tool`:

   ```bash
   dotnet tool update --global dotnet-ef
   ```

   Luego, dentro de la carpeta `Biblioteca digital/`, ejecuta:

   ```bash
   dotnet restore
   dotnet tool restore
   dotnet ef database update
   ```

## Autor

**Marvin Steven Arias Cordero**
Estudiante de Informática Empresarial, Universidad de Costa Rica, Sede Guanacaste.
[github.com/MarvinAriasCordero](https://github.com/MarvinAriasCordero)


#### Tecnologias : 
 C# / .NET8
 SQL Server/SQLlite
 Entity Framework Core
 Identity
 Docker / Docker compose (para pruebas y despliegue local)
