import { Component, signal, ViewEncapsulation,ApplicationConfig} from '@angular/core';
import { RouterOutlet } from '@angular/router'; 
import { Sidebar } from './core/layout/sidebar/sidebar'; 
import { Navbar } from './core/navbar/navbar';
@Component({
  selector: 'app-root',
  imports:[RouterOutlet,Sidebar,Navbar],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  encapsulation: ViewEncapsulation.None
})
export class App {
  protected readonly title = signal('SkylinePayroll.SPA');
}
