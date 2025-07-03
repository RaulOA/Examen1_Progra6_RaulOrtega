/**
 * 🔧 Servicio: TaskService
 * 
 * Proporciona métodos HTTP para interactuar con la API backend de tareas:
 * - Obtener tareas completas o filtradas
 * - Crear, actualizar y eliminar tareas
 * 
 * Utilizado por TaskListComponent para mantener sincronización con la base de datos.
 */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Tarea } from '../models/tarea.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root', // 📌 Este servicio estará disponible globalmente en toda la app
})
export class TaskService {
  private apiUrl = 'https://localhost:7203/api/tareas'; // 🌐 URL base para consumir la API de tareas

  constructor(private http: HttpClient) {}

  // 📥 Obtiene todas las tareas (sin filtros)
  getTareas(): Observable<Tarea[]> {
    return this.http.get<Tarea[]>(this.apiUrl);
  }

  // 🔍 Obtiene tareas aplicando filtros opcionales (estado, orden, búsqueda)
  getTareasConParametros(params: any): Observable<Tarea[]> {
    return this.http.get<Tarea[]>(this.apiUrl, { params });
  }

  // ➕ Crea una nueva tarea enviándola al backend
  agregarTarea(tarea: Tarea): Observable<Tarea> {
    return this.http.post<Tarea>(this.apiUrl, tarea);
  }

  // 🔄 Actualiza una tarea existente mediante PUT (identificada por su ID)
  actualizarTarea(tarea: Tarea): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${tarea.id}`, tarea);
  }

  // 🗑️ Elimina una tarea por ID
  eliminarTarea(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // 🔎 Obtiene una única tarea por su ID (útil para ediciones o vistas detalladas)
  obtenerTareaPorId(id: number): Observable<Tarea> {
    return this.http.get<Tarea>(`${this.apiUrl}/${id}`);
  }
}
