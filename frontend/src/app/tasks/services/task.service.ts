import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

export interface Task {
  id: number;
  nombre: string;
  estado: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = '/api/tareas';
   private tasksSubject = new BehaviorSubject<Task[]>([]);
   tasks$ = this.tasksSubject.asObservable();

   constructor(private http: HttpClient) {}

  loadTasks(): void {
    this.http.get<Task[]>(this.apiUrl)
      .pipe(tap(tasks => this.tasksSubject.next(tasks)))
      .subscribe();
  }
  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  addTask(nombre: string): Observable<Task> {
    const newTask = { nombre, estado: false };
    return this.http.post<Task>(this.apiUrl, newTask)
      .pipe(tap(task => this.tasksSubject.next([...this.tasksSubject.value, task])));
  }

  updateTask(id: number, p0: { estado: boolean; }, task: Task): Observable<Task> {
    const url = `${this.apiUrl}/${task.id}`; // URL de la tarea a actualizar
    return this.http.patch<Task>(url, task).pipe(
      tap(() => this.loadTasks()) // Recargar lista después de actualizar
    );
  }

  updateTaskStatus(id: number, updateData: Partial<Task>): Observable<Task> {
    return this.http.patch<Task>(`${this.apiUrl}/${id}`, updateData);
  }  

  deleteTask(id: number): Observable<Task> {
    return this.http.delete<Task>(`${this.apiUrl}/${id}`);
  }
}
