/**
 * 🧱 Módulo principal de la aplicación Angular
 * 
 * Declara los componentes disponibles, importa los módulos esenciales (Forms, HTTP),
 * e indica cuál es el componente inicial (AppComponent).
 */

// 🧩 Módulo raíz de la aplicación Angular
import { NgModule } from '@angular/core';

// 📦 Permite la ejecución en navegador (aplicación web)
import { BrowserModule } from '@angular/platform-browser';

// ✍️ Para formularios reactivos con [(ngModel)]
import { FormsModule } from '@angular/forms';

// 🌐 Para hacer peticiones HTTP (API REST)
import { HttpClientModule } from '@angular/common/http';

// 🌟 Componente raíz de la aplicación
import { AppComponent } from './app.component';

// ✅ Componente de lista de tareas
import { TaskListComponent } from './components/task-list/task-list.component';

@NgModule({
  // 🔹 Componentes que pertenecen a este módulo
  declarations: [AppComponent, TaskListComponent],

  // 🔸 Módulos externos que se importan para usar sus funcionalidades
  imports: [
    BrowserModule, // Necesario para que la app se renderice en un navegador
    FormsModule, // Permite uso de ngModel y formularios
    HttpClientModule, // Permite hacer peticiones HTTP a APIs externas
  ],

  // 🧰 Servicios que están disponibles globalmente (no usado aquí)
  providers: [],

  // 🚀 Componente inicial que se carga en la aplicación
  bootstrap: [AppComponent],
})
export class AppModule {}
