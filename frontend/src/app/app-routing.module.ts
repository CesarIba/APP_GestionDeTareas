import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksModule } from './tasks/tasks.module';


const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },
  { path: 'tasks', loadChildren: () => import('./tasks/tasks.module').then(m => m.TasksModule) }
];

@NgModule({
  imports: [TasksModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
