import { Routes } from '@angular/router';
import { Viewer3dComponent } from './components/viewer3d/viewer3d';

export const routes: Routes = [
  { path: '', component: Viewer3dComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '' }
];
