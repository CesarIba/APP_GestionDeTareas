import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';
import { TaskService } from '../services/task.service';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let taskService: jasmine.SpyObj<TaskService>;

  beforeEach(async () => {
    taskService = jasmine.createSpyObj('TaskService', ['addTask', 'getTasks']);
    await TestBed.configureTestingModule({
      imports: [FormsModule, TaskListComponent, HttpClientTestingModule], // Agregar módulos necesarios
      declarations: [],
      providers: [{ provide: TaskService, useValue: taskService }],
    }).compileComponents();

    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    taskService = TestBed.inject(TaskService) as jasmine.SpyObj<TaskService>;
  });
  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    // Mock para que tasks$ no sea undefined
    taskService.getTasks.and.returnValue(of([
      { id: 1, nombre: 'Tarea 1', estado: false },
      { id: 2, nombre: 'Tarea 2', estado: true }
    ]));
    // Asegurar que el servicio devuelve un Observable al llamar addTask
    taskService.addTask.and.returnValue(of({ id: 1, nombre: 'Nueva tarea', estado: false }));

    fixture.detectChanges();
  });

  it('debería agregar una tarea y limpiar el input', () => {
    component.newTaskTitle = 'Nueva tarea test'; // Simulamos una entrada de usuario
    component.addTask(); // Llamamos al método

    expect(taskService.addTask).toHaveBeenCalledWith('Nueva tarea test'); // Verifica que se llamó al servicio
    expect(component.newTaskTitle).toBe(''); // Verifica que se limpió el input
  });
});
