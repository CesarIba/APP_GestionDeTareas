import { Component, ChangeDetectionStrategy, OnInit, ChangeDetectorRef  } from '@angular/core';
import { TaskService, Task } from '../services/task.service';
import { map, Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,  
  imports: [CommonModule, FormsModule],
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush // 🚀 Optimización
})
export class TaskListComponent implements OnInit {
  tasks$: Observable<Task[]> = new Observable();
  filteredTasks$!: Observable<Task[]>;
  filterStatus: string = 'all';
  newTaskTitle = '';

  constructor(private taskService: TaskService, private cdr: ChangeDetectorRef) {
    //this.tasks$ = this.taskService.tasks$;
  }

  loadTasks() {
    this.tasks$ = this.taskService.getTasks(); // Recargar tareas
  }
  ngOnInit(): void {
    this.tasks$ = this.taskService.getTasks();
    this.cdr.detectChanges();
    this.applyFilter();
  }

  applyFilter() {
    this.filteredTasks$ = this.tasks$.pipe(
      map(tasks => {
        if (this.filterStatus === 'completed') {
          return tasks.filter(task => task.estado);
        } else if (this.filterStatus === 'pending') {
          return tasks.filter(task => !task.estado);
        }
        return tasks;
      })
    );
  }

  addTask() {
    if (!this.newTaskTitle.trim()) return; // Evitar envíos vacíos

    this.taskService.addTask(this.newTaskTitle).subscribe(() => {
      this.loadTasks();  // Recargar lista de tareas
      this.newTaskTitle = ''; // Limpiar el input
      this.applyFilter();
      this.cdr.detectChanges(); // Forzar actualización de la vista    
    });
  }

  deleteTask(id: number) {
    if (confirm('¿Estás seguro de eliminar esta tarea?')) {
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks(); 
        this.applyFilter();
        this.cdr.markForCheck(); 
      });
    }
  }

  completeTask(task: Task) {
    this.taskService.updateTaskStatus(task.id, { estado: !task.estado }).subscribe(() => {
      this.loadTasks(); // Asegura que se recargue la lista después de la actualización
      this.applyFilter();  
      this.cdr.detectChanges(); // Forzar actualización de la vista  
    });
  }   
}
