import { Component, signal } from '@angular/core';
import { ValueComponent } from "./value/value.component";
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-root',
  standalone: true, // Standalone olduğunu belirtiyoruz
  imports: [ValueComponent,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('SehirRehberi.SPA');
}

