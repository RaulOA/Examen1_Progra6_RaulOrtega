/**
 * 🧱 Modelo de dominio: Tarea
 *
 * Esta clase representa la estructura de una entidad de tarea dentro del sistema.
 * Es utilizada por Entity Framework Core (EF Core) para mapear automáticamente esta clase
 * a una tabla en la base de datos (por convención, "Tareas").
 *
 * Esta clase también define el esquema que se expone a través de la API, y por lo tanto
 * debe estar alineada con los modelos usados por el frontend (como la interfaz `Tarea` en Angular).
 */

namespace TodoApp.Data.Models
{
    public class Tarea
    {
        /// <summary>
        /// Identificador único de la tarea (clave primaria).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título obligatorio de la tarea (nombre breve).
        /// </summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Descripción opcional con más detalles sobre la tarea.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Fecha límite en que la tarea debe completarse.
        /// </summary>
        public DateTime FechaLimite { get; set; }

        /// <summary>
        /// Estado de finalización (true = completada).
        /// </summary>
        public bool EstaCompletada { get; set; }

        /// <summary>
        /// Prioridad de la tarea (1 = alta, 2 = media, 3 = baja).
        /// </summary>
        public int Prioridad { get; set; }
    }
}