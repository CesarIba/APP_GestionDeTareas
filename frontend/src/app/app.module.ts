import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module'; 
import { TaskService } from './tasks/services/task.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent], // ✅ AppComponent debe estar aquí
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule  // ✅ Importamos las rutas
  ],
  providers: [TaskService],
  bootstrap: [AppComponent]
})
export class AppModule { }
