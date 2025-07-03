/**
 * 🧩 DbContext: TareasDbContext
 * 
 * Esta clase representa el "contexto de base de datos" de Entity Framework Core para la aplicación.
 * Se encarga de mapear el modelo `Tarea` a una tabla SQL y proporciona acceso directo para consultar
 * y modificar registros desde el código en C#.
 * 
 * Se registra en `Program.cs` mediante inyección de dependencias y es utilizada internamente
 * por los controladores para interactuar con los datos.
 */

// ✅ Requiere instalación de NuGet: Microsoft.EntityFrameworkCore
// Provee las clases base para usar EF Core, como DbContext y DbSet<T>
using Microsoft.EntityFrameworkCore;

// 📦 Namespace interno del proyecto que contiene el modelo de datos 'Tarea'
using TodoApp.Data.Models;

namespace TodoApp.Data
{
    /**
     * 🔧 Contexto EF Core: maneja la conexión a la base de datos y expone las entidades
     * que serán mapeadas a tablas relacionales.
     */
    public class TareasDbContext : DbContext
    {
        /**
         * 🧪 Constructor que recibe las opciones de configuración (cadena de conexión, proveedor, etc.)
         * Este constructor será llamado automáticamente al usar inyección de dependencias.
         */
        public TareasDbContext(DbContextOptions<TareasDbContext> options) : base(options) { }

        /**
         * 🗃️ Propiedad de acceso a la tabla de tareas.
         * Permite realizar operaciones como: _context.Tareas.ToList(), .Add(), .Remove(), etc.
         */
        public DbSet<Tarea> Tareas => Set<Tarea>();

        /**
         * ⚙️ Configuración del modelo: define manualmente el nombre de la tabla en SQL Server.
         * Es útil si querés evitar nombres automáticos como "Tareas" vs. "Tarea".
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>().ToTable("Tareas");
        }
    }
}