import { Component } from '@angular/core';
import { provideRouter, RouterModule, Routes } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `<router-outlet></router-outlet>`,
  imports: [RouterModule]
})
export class AppComponent { }
