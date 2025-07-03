/**
 * 🎯 Componente principal: TaskListComponent
 *
 * Se encarga de gestionar la lógica de presentación de la lista de tareas:
 * - Muestra tareas filtradas según estado, prioridad y texto de búsqueda
 * - Permite crear, editar, eliminar y completar tareas
 * - Se comunica con el backend a través del servicio TaskService
 * 
 * Asociado a la plantilla HTML y estilos CSS en la carpeta de componentes.
 */

import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { Tarea } from '../../models/tarea.model';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})
export class TaskListComponent implements OnInit {
  // 🔹 Lista principal de tareas cargadas desde el backend
  tareas: Tarea[] = [];

  // 🔸 Objeto vinculado al formulario de creación
  nuevaTarea: Tarea = {
    id: 0,
    titulo: '',
    descripcion: '',
    fechaLimite: '',
    estaCompletada: false,
    prioridad: 2, // Por defecto: media prioridad
  };

  // 🔎 Filtros visuales aplicados desde la interfaz
  filtroEstado: string = '';
  ordenSeleccionado: string = '';
  textoBusqueda: string = '';

  // 📊 Estadísticas dinámicas
  totalCompletadas: number = 0;
  totalPendientes: number = 0;

  // ✏️ Variable auxiliar usada durante la edición de tareas
  tareaEnEdicion: Tarea | null = null;

  constructor(private taskService: TaskService) {}

  // 🚀 Se ejecuta al inicializar el componente (carga inicial)
  ngOnInit(): void {
    this.cargarTareas();
    this.actualizarEstadisticas();
  }

  // 📥 Consulta todas las tareas desde el backend
  cargarTareas(): void {
    this.taskService.getTareas().subscribe({
      next: (tareas) => (this.tareas = tareas),
      error: (err) => console.error('Error al obtener tareas:', err),
    });
  }

  // ➕ Agrega una nueva tarea usando el formulario de creación
  crearTarea(): void {
    this.taskService.agregarTarea(this.nuevaTarea).subscribe({
      next: (tareaCreada) => {
        this.tareas.push(tareaCreada); // Se agrega directamente a la lista
        // Resetea el formulario
        this.nuevaTarea = {
          id: 0,
          titulo: '',
          descripcion: '',
          fechaLimite: '',
          estaCompletada: false,
          prioridad: 2,
        };
      },
      error: (err) => console.error('Error al crear tarea:', err),
    });
  }

  // ✏️ Habilita edición para una tarea específica
  iniciarEdicion(tarea: Tarea): void {
    this.tareaEnEdicion = { ...tarea }; // Se clona para no modificar directamente
  }

  // ❌ Cancela la edición y descarta cambios
  cancelarEdicion(): void {
    this.tareaEnEdicion = null;
  }

  // 💾 Guarda los cambios realizados en una tarea editada
  guardarEdicion(): void {
    const tarea = this.tareaEnEdicion;
    if (!tarea) return;

    this.taskService.actualizarTarea(tarea).subscribe({
      next: () => {
        const index = this.tareas.findIndex((t) => t.id === tarea.id);
        if (index !== -1) {
          this.tareas[index] = {
            id: tarea.id,
            titulo: tarea.titulo,
            descripcion: tarea.descripcion ?? '',
            fechaLimite: tarea.fechaLimite,
            estaCompletada: tarea.estaCompletada,
            prioridad: tarea.prioridad,
          };
        }
        this.tareaEnEdicion = null;
      },
      error: (err) => console.error('Error al actualizar tarea:', err),
    });
  }

  // 🗑️ Elimina una tarea tras confirmación del usuario
  eliminarTarea(id: number): void {
    if (!confirm('¿Seguro que deseas eliminar esta tarea?')) return;

    this.taskService.eliminarTarea(id).subscribe({
      next: () => {
        this.tareas = this.tareas.filter((t) => t.id !== id);
      },
      error: (err) => console.error('Error al eliminar tarea:', err),
    });
  }

  // ✔️ Marca una tarea como completada (actualiza en backend)
  marcarComoCompletada(tarea: Tarea): void {
    const tareaActualizada = { ...tarea, estaCompletada: true };

    this.taskService.actualizarTarea(tareaActualizada).subscribe({
      next: () => {
        const index = this.tareas.findIndex((t) => t.id === tarea.id);
        if (index !== -1) {
          this.tareas[index] = tareaActualizada;
        }
      },
      error: (err) => console.error('Error al marcar como completada:', err),
    });
  }

  // 🎛️ Aplica los filtros visuales seleccionados por el usuario
  aplicarFiltros(): void {
    const params: any = {};

    if (this.filtroEstado) params.estado = this.filtroEstado;
    if (this.ordenSeleccionado) params.orden = this.ordenSeleccionado;
    if (this.textoBusqueda.trim()) params.buscar = this.textoBusqueda.trim();

    this.taskService.getTareasConParametros(params).subscribe({
      next: (tareas) => {
        this.tareas = tareas;
        this.actualizarEstadisticas(); // Recalcula resumen tras filtro
      },
      error: (err) => console.error('Error al aplicar filtros:', err),
    });
  }

  // 📊 Actualiza los contadores de tareas completadas y pendientes
  actualizarEstadisticas(): void {
    this.totalCompletadas = this.tareas.filter((t) => t.estaCompletada).length;
    this.totalPendientes = this.tareas.filter((t) => !t.estaCompletada).length;
  }
}
