# Biblioteca Digital

API REST para la gestión de una biblioteca digital, desarrollada con C# y .NET 8. Permite registrar usuarios e iniciar sesión con JWT, administrar libros (solo el rol administrador puede crearlos) y controlar los préstamos y devoluciones.


## Tecnologías

- C# / .NET 8 y ASP.NET Core Web API
- Entity Framework Core 8 (migraciones code-first)
- ASP.NET Core Identity con roles (`Admin` y `User`)
- Autenticación con tokens JWT
- SQL Server
- Swagger (Swashbuckle)
- Docker y Docker Compose

## Estructura del proyecto

| Carpeta | Contenido |
|---|---|
| `Controllers/` | Endpoints de la API (autenticación, libros y préstamos) |
| `Servicios/` | Lógica de autenticación y generación de tokens |
| `Database/` | Contexto de Entity Framework, repositorio genérico y configuración |
| `Dtos/` | Modelos de entrada y salida de la API |
| `Model/` | Entidades: `Libro`, `Prestamo` y `Usuario` |
| `Migrations/` | Migraciones de la base de datos |

## Endpoints

| Método | Endpoint | Acceso | Descripción |
|---|---|---|---|
| POST | `api/v1/auth/register` | Público | Registra un usuario (con rol `User`) |
| POST | `api/v1/auth/login` | Público | Inicia sesión y devuelve un token JWT |
| GET | `api/v1/books?page=1&limit=10` | Token | Lista los libros con paginación |
| POST | `api/v1/books` | Token, rol `Admin` | Crea un libro |
| POST | `api/v1/loans` | Token | Registra el préstamo de un libro |
| POST | `api/v1/loans/withdraw` | Token | Registra la devolución de un libro (solo quien lo pidió prestado) |

Los endpoints protegidos se llaman con la cabecera `Authorization: Bearer <token>`.

### Ejemplo de uso

```bash
# 1. Registrar un usuario
curl -X POST http://localhost:5106/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"name":"usuario1","email":"usuario1@correo.com","password":"Clave123!","passwordConfirm":"Clave123!"}'

# 2. Iniciar sesión (la respuesta incluye el token)
curl -X POST http://localhost:5106/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"usuario1@correo.com","password":"Clave123!"}'

# 3. Listar libros con el token
curl "http://localhost:5106/api/v1/books?page=1&limit=10" \
  -H "Authorization: Bearer <token>"
```

## Cómo ejecutarlo

Primero, clona el repositorio:

```bash
git clone https://github.com/MarvinAriasCordero/Biblioteca-Digital.git
cd Biblioteca-Digital
```

### Opción 1: Docker y Docker Compose

Levanta la API junto con un contenedor de SQL Server. Está configurado pensando en Windows (el archivo `docker-compose.override.yml` usa la variable `APPDATA`).

```bash
docker-compose build
docker-compose up
```

La API queda en `http://localhost:5106` y la documentación de Swagger en `http://localhost:5106/swagger`. En el entorno `Development`, las migraciones se aplican automáticamente al iniciar.

### Opción 2: Sin Docker (SQL Server instalado localmente)

1. Ajusta la cadena de conexión `ConnectionStrings:instance_db` para que apunte a tu SQL Server (en `appsettings.Development.json` o con `dotnet user-secrets`).
2. Aplica las migraciones y ejecuta el proyecto:

   ```bash
   dotnet tool update --global dotnet-ef
   cd "Biblioteca digital"
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

## Configuración

El secreto del JWT (`ApiSettings:JwtOptions:secret`) y la cadena de conexión a la base de datos (`ConnectionStrings:instance_db`) son datos sensibles. Para cualquier uso fuera de pruebas locales, cámbialos y configúralos con variables de entorno (por ejemplo, `ApiSettings__JwtOptions__secret` y `ConnectionStrings__instance_db`), no en el repositorio.

La migración inicial crea los roles `Admin` y `User` y un usuario administrador de prueba (ver `Database/config/UserConfig.cs`). Cambia sus credenciales antes de desplegar.

## Autor

**Marvin Steven Arias Cordero**
Estudiante de Informática Empresarial, Universidad de Costa Rica, Sede Guanacaste.
[github.com/MarvinAriasCordero](https://github.com/MarvinAriasCordero)

 Identity
 Docker / Docker compose (para pruebas y despliegue local)
