import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { AppComponent } from './app/app.component';
import { TASKS_ROUTES } from './app/tasks/task.routes';

bootstrapApplication(AppComponent, {
  providers: [provideRouter([{ path: 'tasks', loadChildren: () => TASKS_ROUTES }]), provideHttpClient()]
});
