/**
 * 📄 Modelo de datos: Tarea
 * 
 * Representa la estructura de una tarea dentro del sistema, tanto para lectura como escritura.
 * Se utiliza en toda la aplicación Angular (componentes y servicios) para tipado y validación.
 * Este modelo debe estar sincronizado con la entidad en el backend (.NET).
 */

export interface Tarea {
  /** Identificador único de la tarea (clave primaria) */
  id: number;

  /** Título breve y descriptivo de la tarea */
  titulo: string;

  /** Descripción opcional con más detalles de la tarea */
  descripcion?: string;

  /** Fecha límite esperada (en formato ISO string) */
  fechaLimite: string;

  /** Estado de la tarea: true si ya está completada */
  estaCompletada: boolean;

  /** Nivel de prioridad:
   * 1 = Alta (urgente)
   * 2 = Media (por defecto)
   * 3 = Baja (sin urgencia)
   */
  prioridad: number;
}
