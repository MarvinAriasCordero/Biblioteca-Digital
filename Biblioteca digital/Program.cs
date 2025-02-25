using Biblioteca_digital.Database;
using Biblioteca_digital.Database.config;
using Biblioteca_digital.Model;
using Biblioteca_digital.Utilitarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configuración de Entity Framework

var connectionString = builder.Configuration.GetConnectionString("instance_db");
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
  options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<BibliotecaDbContext>()
    //.AddUserManager<User>()
    //.AddRoleManager<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions");
//configure autentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions["Issuer"],
            ValidAudience = jwtOptions["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["secret"]))
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository();


builder.Services.AddAutneticatorServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    ServicioMigracionDB.DataBaseMigrationInitialization(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
