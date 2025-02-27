import { Component, Input } from '@angular/core';
import { TaskService, Task } from '../services/task.service';

@Component({
  selector: 'app-task-item',
  templateUrl: './task-item.component.html',
  styleUrls: ['./task-item.component.scss']
})
export class TaskItemComponent {
  @Input() task!: Task;

  constructor(private taskService: TaskService) {}

  toggleTask() {
    this.taskService.updateTaskStatus(this.task.id, { estado: this.task.estado }).subscribe();
  }

  deleteTask() {
    this.taskService.deleteTask(this.task.id).subscribe();
  }
}
