/**
 * 📦 Migración EF Core: Inicial
 *
 * Esta clase representa la primera migración generada por Entity Framework Core
 * para crear la estructura inicial de la base de datos.
 * 
 * Su propósito es crear la tabla "Tareas" con todas sus columnas, tipos de datos
 * y restricciones definidas en el modelo `Tarea`.
 * 
 * EF Core utiliza esta clase para aplicar (Up) o revertir (Down) cambios estructurales
 * en la base de datos mediante los comandos:
 *   - dotnet ef database update
 *   - dotnet ef migrations remove
 *   - dotnet ef database update [migración]
 */

// ✅ Librería base del sistema .NET (ya incluida en el SDK)
using System;

// ✅ Requiere NuGet: Microsoft.EntityFrameworkCore.Relational
// Permite crear y manipular estructuras de migración en EF Core
using Microsoft.EntityFrameworkCore.Migrations;

// #nullable disable desactiva los warnings de valores nulos en este archivo
#nullable disable

// 🗂️ Espacio de nombres para organizar las migraciones del proyecto
namespace TodoApp.Data.Migrations
{
    /// <summary>
    /// 🧱 Clase parcial generada para aplicar la migración "Inicial"
    /// </summary>
    public partial class Inicial : Migration
    {
        /// <summary>
        /// 🆙 Método que define los cambios a aplicar al ejecutar la migración.
        /// En este caso, crea la tabla "Tareas" con sus columnas.
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas", // Nombre exacto de la tabla SQL
                columns: table => new
                {
                    // ⚙️ Clave primaria con autoincremento
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // 📌 Campo obligatorio de texto
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    // 📄 Campo de texto opcional
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // 📅 Fecha límite (datetime2 en SQL Server)
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: false),

                    // ✅ Estado de completado (bool → bit en SQL)
                    EstaCompletada = table.Column<bool>(type: "bit", nullable: false),

                    // 🔺 Prioridad numérica (1 a 3)
                    Prioridad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    // 🔑 Definición de la clave primaria
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                });
        }

        /// <summary>
        /// 🔽 Método que define cómo revertir esta migración (Rollback).
        /// En este caso, elimina la tabla "Tareas".
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");
        }
    }
}