# Backend - APP_GestionDeTareas

Este es el backend de la aplicación APP_GestionDeTareas, desarrollado en .NET CORE 

## Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [SQLite](https://www.sqlite.org/download.html) (Opcional, .NET maneja SQLite internamente)

## 🔧 Instalación y Ejecución

Abrir una terminal en la carpeta `.\backend\BackGestionTareas\` y ejecuta:
1. Restaurar dependencias:
	```bash
   dotnet restore
   ```
   
2. Aplicar Migraciones para generar la base de datos SQLite  
   	```bash
   dotnet ef database update
   ```
   
3. Ejecutar el backend   
   	```bash
   dotnet run
   ```
   
4. Probar la API
	API disponible en: https://localhost:5000/swagger

5. Para ejecutar las pruebas del backend:
	```bash
	cd ../Test
	dotnet test
	```

Este backend usa SQLite, y la base de datos se guarda en un archivo local (app.db).
Si se requiere modificar la configuración, revisa el archivo appsettings.json:	
"ConnectionStrings": {
    "DefaultConnection": "Data Source=tareas.db"
  }

	
Endpoints Principales
Método	Endpoint			Descripción
GET		/api/tareas			Obtener todas las tareas
POST	/api/tareas			Crear una nueva tarea
PATCH	/api/tareas/{id}	Actualizar estado de una tarea
DELETE	/api/tareas/{id}	Eliminar una tarea