import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Viewer3dComponent } from './components/viewer3d/viewer3d';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, Viewer3dComponent],
  template: `
    <main class="main-content">
      <app-viewer3d></app-viewer3d>
    </main>
  `,
  styles: [`
    :host {
      display: block;
      height: 100vh;
      width: 100vw;
      overflow: hidden;
    }
    
    .main-content {
      height: 100%;
      width: 100%;
      display: flex;
      flex-direction: column;
    }
  `]
})
export class App {
  title = '3D Viewer';
}
