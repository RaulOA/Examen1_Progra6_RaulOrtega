/**
 * 🎯 Controlador API: TareasController
 *
 * Este controlador expone endpoints HTTP RESTful para gestionar tareas dentro de la aplicación.
 * Permite realizar operaciones CRUD (crear, leer, actualizar y eliminar) mediante rutas como:
 *  - GET    /api/tareas
 *  - GET    /api/tareas/{id}
 *  - POST   /api/tareas
 *  - PUT    /api/tareas/{id}
 *  - DELETE /api/tareas/{id}
 *
 * Se conecta al contexto de base de datos `TareasDbContext` mediante inyección de dependencias.
 * Utiliza Entity Framework Core (EF Core) como ORM para acceso y manipulación de datos.
 *
 * Este archivo forma parte de una solución ASP.NET Core Web API y fue generado/modificado en Visual Studio 2022.
 */

// ✅ Namespace de ASP.NET Core para atributos de controlador y respuestas HTTP
using Microsoft.AspNetCore.Mvc; // Nativo de .NET SDK (no requiere instalación desde NuGet)

// ✅ Namespace de Entity Framework Core para acceso a base de datos
using Microsoft.EntityFrameworkCore; // Requiere instalación de NuGet: Microsoft.EntityFrameworkCore

// 👇 Referencias a archivos propios del proyecto
using TodoApp.Data;         // Archivo local: clase del DbContext (conexión a la base de datos)
using TodoApp.Data.Models; // Archivo local: contiene la clase Tarea (modelo de datos)

[ApiController] // 📌 Indica que esta clase responde como controlador REST (valida y formatea respuestas automáticamente)
[Route("api/[controller]")] // 📦 Ruta base: api/tareas (usa el nombre del controlador)
public class TareasController : ControllerBase
{
    // 🔗 Inyección del contexto EF Core para acceder a la base de datos
    private readonly TareasDbContext _context;

    public TareasController(TareasDbContext context)
    {
        _context = context;
    }

    // 📥 GET: api/tareas
    // Devuelve una lista de tareas filtradas por estado, búsqueda o tipo de ordenamiento (opcional)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas(
        string? estado = null,
        string? orden = null,
        string? buscar = null)
    {
        var query = _context.Tareas.AsQueryable();

        // 🎯 Filtro por estado (pendiente, completada)
        if (!string.IsNullOrEmpty(estado))
        {
            if (estado.ToLower() == "completada")
                query = query.Where(t => t.EstaCompletada);
            else if (estado.ToLower() == "pendiente")
                query = query.Where(t => !t.EstaCompletada);
        }

        // 🔍 Búsqueda por texto en título o descripción
        if (!string.IsNullOrEmpty(buscar))
        {
            query = query.Where(t =>
                t.Titulo.Contains(buscar) ||
                (t.Descripcion != null && t.Descripcion.Contains(buscar)));
        }

        // 🔀 Orden por fecha límite o prioridad
        if (!string.IsNullOrEmpty(orden))
        {
            if (orden.ToLower() == "fecha")
                query = query.OrderBy(t => t.FechaLimite);
            else if (orden.ToLower() == "prioridad")
                query = query.OrderBy(t => t.Prioridad);
        }

        return await query.ToListAsync(); // Ejecuta la consulta de forma asincrónica
    }

    // 📥 GET: api/tareas/{id}
    // Devuelve una sola tarea por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarea>> GetTarea(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        return tarea is null ? NotFound() : tarea;
    }

    // ➕ POST: api/tareas
    // Crea una nueva tarea en la base de datos
    [HttpPost]
    public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
    {
        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();

        // Devuelve 201 Created con la ubicación del recurso nuevo
        return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
    }

    // 🔄 PUT: api/tareas/{id}
    // Actualiza completamente una tarea existente (por ID)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarea(int id, Tarea tarea)
    {
        if (id != tarea.Id)
            return BadRequest(); // El ID de la URL no coincide con el objeto recibido

        // Marca la entidad como modificada en el contexto
        _context.Entry(tarea).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Si la tarea ya no existe, devuelve 404
            if (!_context.Tareas.Any(t => t.Id == id))
                return NotFound();
            else
                throw; // Otro error de concurrencia: relanza
        }

        return NoContent(); // 204 No Content si fue exitoso
    }

    // 🗑️ DELETE: api/tareas/{id}
    // Elimina una tarea por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarea(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea is null)
            return NotFound();

        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}