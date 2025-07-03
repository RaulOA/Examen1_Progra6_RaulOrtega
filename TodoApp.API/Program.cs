/**
 * 🚀 Punto de entrada principal del backend ASP.NET Core Web API
 * 
 * Este archivo configura y lanza la aplicación:
 * - Registra servicios (como controladores, CORS, Swagger, EF Core)
 * - Define cómo se comporta la aplicación al iniciarse
 * - Expone los endpoints del backend (como api/tareas) usando MapControllers()
 * 
 * Compatible con ASP.NET Core 6+ (estructura minimalista con clase Program como raíz).
 */

// 📦 Acceso al contexto de datos (EF Core personalizado para Tareas)
using TodoApp.Data;

// ✅ Requiere NuGet: Microsoft.EntityFrameworkCore y Microsoft.EntityFrameworkCore.SqlServer
// Permite configurar EF Core como motor ORM y usar SQL Server
using Microsoft.EntityFrameworkCore;

namespace TodoApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 🏗️ Crea el constructor de la aplicación (WebApplicationBuilder)
            var builder = WebApplication.CreateBuilder(args);

            // 🧩 Configura el contexto EF Core usando SQL Server
            // Usa la cadena de conexión definida en appsettings.json → "DefaultConnection"
            builder.Services.AddDbContext<TareasDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ Habilita el soporte para controladores con atributos como [HttpGet]
            builder.Services.AddControllers();

            // 📘 Documentación Swagger/OpenAPI para explorar la API desde navegador
            builder.Services.AddEndpointsApiExplorer(); // Registra metadata de endpoints
            builder.Services.AddSwaggerGen();           // Genera la UI interactiva de Swagger

            // 🔓 Configuración de CORS (Cross-Origin Resource Sharing)
            // Esto permite que tu frontend Angular (http://localhost:4200) consuma la API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // ⚠️ Asegurate de que coincida con la URL de Angular
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 🛠️ Crea la instancia final de la aplicación lista para ejecutarse
            var app = builder.Build();

            // 🟢 Aplica la política CORS para permitir llamadas desde Angular
            app.UseCors("AllowAngularApp");

            // ⚙️ Configura el middleware en tiempo de desarrollo (Swagger UI)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();     // Habilita el endpoint swagger.json
                app.UseSwaggerUI();   // Habilita la interfaz visual (Swagger Explorer)
            }

            // 🔐 Redirecciona todas las llamadas HTTP a HTTPS
            app.UseHttpsRedirection();

            // 🧾 Middleware de autorización (aunque no se define autenticación en este proyecto)
            app.UseAuthorization();

            // 🧭 Mapea todos los controladores [ApiController] como rutas disponibles
            app.MapControllers();

            // 🚀 Inicia la aplicación web
            app.Run();
        }
    }
}