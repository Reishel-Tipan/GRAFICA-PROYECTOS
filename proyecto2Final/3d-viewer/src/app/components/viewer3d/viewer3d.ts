import { Component, ElementRef, ViewChild, AfterViewInit, OnDestroy, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SceneManager, ShapeType } from '../../models/scene-manager';

@Component({
  selector: 'app-viewer3d',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './viewer3d.html',
  styleUrls: ['./viewer3d.css']
})
export class Viewer3dComponent implements AfterViewInit, OnDestroy {
  @ViewChild('canvas', { static: true }) private canvasRef!: ElementRef<HTMLCanvasElement>;
  
  private sceneManager!: SceneManager;
  
  // Valores para los controles
  selectedShape: ShapeType = 'cube';
  color: string = '#00ff00'; // Verde por defecto
  rotation = { x: 0, y: 0, z: 0 };
  position = { x: 0, y: 0, z: 0 };
  scale: number = 1.0;
  isRotating: boolean = false;
  showControls: boolean = true; // Forzado a true para depuración
  isMobileView = false;
  showTitleScreen: boolean = true; // Controla si mostrar la pantalla de título
  private resizeTimeout: any;
  private resizeObserver: ResizeObserver | null = null;

  constructor() {
    this.checkIfMobile();
    // Detectar cambios en la orientación del dispositivo
    window.addEventListener('orientationchange', this.handleOrientationChange.bind(this));
  }

  shapes: { value: ShapeType; label: string }[] = [
    { value: 'cube', label: 'Cubo' },
    { value: 'sphere', label: 'Esfera' },
    { value: 'cone', label: 'Cono' },
    { value: 'cylinder', label: 'Cilindro' },
    { value: 'pyramid', label: 'Pirámide' }
  ];

  ngAfterViewInit() {
    this.sceneManager = new SceneManager(this.canvasRef.nativeElement);
    // Asegurar que la figura comience en verde
    this.sceneManager.setShape(this.selectedShape);
    this.sceneManager.setColor('#00ff00'); // Forzar color verde al inicio
    this.color = '#00ff00'; // Sincronizar el selector de color
    this.updateScene();
    this.setupResizeObserver();
    
    // Mostrar siempre los controles
    this.showControls = true;
    
    // Forzar un redimensionamiento inicial
    setTimeout(() => this.onResize(), 0);
    
    // Asegurar que el canvas tenga el foco para los controles de teclado
    this.canvasRef.nativeElement.focus();
  }

  ngOnDestroy() {
    // Limpiar el ResizeObserver
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
      this.resizeObserver = null;
    }
    
    // Limpiar el timeout de redimensionamiento
    if (this.resizeTimeout) {
      clearTimeout(this.resizeTimeout);
    }
    
    // Limpiar el SceneManager
    if (this.sceneManager) {
      this.sceneManager.ngOnDestroy();
    }
    
    // Eliminar el event listener de cambio de orientación
    window.removeEventListener('orientationchange', this.handleOrientationChange.bind(this));
  }

  @HostListener('window:resize')
  onResize() {
    // Usar debounce para evitar múltiples llamadas durante el redimensionamiento
    if (this.resizeTimeout) {
      clearTimeout(this.resizeTimeout);
    }
    
    this.resizeTimeout = setTimeout(() => {
      this.handleResize();
    }, 100);
  }
  
  private handleResize() {
    this.checkIfMobile();
    this.showControls = true; // Mostrar siempre los controles
    
    // Forzar un redibujado del canvas
    if (this.sceneManager) {
      // Usar setTimeout para asegurar que los estilos CSS se hayan aplicado
      setTimeout(() => {
        const canvas = this.canvasRef.nativeElement;
        const container = this.canvasRef.nativeElement.parentElement;
        
        if (container) {
          // Calcular el ancho disponible restando el ancho del panel de controles
          const panelWidth = this.isMobileView ? 0 : 320;
          const availableWidth = container.clientWidth - panelWidth;
          
          // Asegurar que el canvas tenga el tamaño correcto
          canvas.style.width = `${availableWidth}px`;
          canvas.style.height = `${container.clientHeight}px`;
          
          // Notificar al SceneManager del cambio de tamaño
          this.sceneManager.onWindowResize();
        }
      }, 0);
    }
  }
  
  private handleOrientationChange() {
    // Forzar un redimensionamiento después de un cambio de orientación
    setTimeout(() => {
      this.handleResize();
    }, 300);
  }
  
  private setupResizeObserver() {
    if (typeof ResizeObserver !== 'undefined') {
      this.resizeObserver = new ResizeObserver(entries => {
        for (const entry of entries) {
          if (entry.contentBoxSize) {
            this.handleResize();
          }
        }
      });
      
      if (this.canvasRef?.nativeElement) {
        this.resizeObserver.observe(this.canvasRef.nativeElement);
      }
    }
  }

  private checkIfMobile() {
    // Detectar si es un dispositivo móvil
    const isMobileScreen = window.innerWidth <= 1024;
    const isMobileUserAgent = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
    this.isMobileView = isMobileScreen || isMobileUserAgent;
    this.showControls = true;
  }

  private setViewportMeta() {
    // Asegurar que el viewport esté configurado correctamente para móviles
    const viewportMeta = document.querySelector('meta[name="viewport"]');
    if (this.isMobileView) {
      if (viewportMeta) {
        viewportMeta.setAttribute('content', 'width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no, viewport-fit=cover');
      }
    } else if (viewportMeta) {
      viewportMeta.setAttribute('content', 'width=device-width, initial-scale=1.0');
    }
  }

  resetScale() {
    this.scale = 1.0;
    this.sceneManager.setScale(this.scale);
  }

  onShapeChange(): void {
    this.sceneManager.setShape(this.selectedShape);
    // Asegurar que la nueva figura también sea verde
    this.sceneManager.setColor('#00ff00');
    this.color = '#00ff00'; // Actualizar el selector de color
    
    // Ocultar pantalla de título al seleccionar una figura
    if (this.showTitleScreen) {
      this.showTitleScreen = false;
      // Asegurarse de que los controles estén visibles
      this.showControls = true;
    }
  }

  onColorChange() {
    this.sceneManager.setColor(this.color);
  }

  onRotationChange() {
    // Aseguramos que los valores estén dentro del rango [-180, 180]
    this.rotation.x = ((this.rotation.x % 360) + 540) % 360 - 180;
    this.rotation.y = ((this.rotation.y % 360) + 540) % 360 - 180;
    this.rotation.z = ((this.rotation.z % 360) + 540) % 360 - 180;
    
    // Aplicamos la rotación
    this.sceneManager.setRotation(
      this.rotation.x,
      this.rotation.y,
      this.rotation.z
    );
  }

  onPositionChange() {
    this.sceneManager.setPosition(this.position.x, this.position.y, this.position.z);
  }

  onScaleChange() {
    this.sceneManager.setScale(this.scale);
  }

  toggleRotation() {
    this.sceneManager.toggleRotation(this.isRotating);
  }

  resetCamera() {
    if (this.sceneManager) {
      this.sceneManager.setPosition(0, 0, 0);
      this.position = { x: 0, y: 0, z: 0 };
      this.onPositionChange();
    }
  }

  resetRotation() {
    // Usamos requestAnimationFrame para asegurar que se actualice en el siguiente frame
    requestAnimationFrame(() => {
      this.rotation = { x: 0, y: 0, z: 0 };
      this.sceneManager.setRotation(0, 0, 0);
      // Forzamos la actualización de la vista
      this.sceneManager['renderer'].render(this.sceneManager['scene'], this.sceneManager['camera']);
    });
  }

  resetAll() {
    this.selectedShape = 'cube';
    this.color = '#00ff00';
    this.rotation = { x: 0, y: 0, z: 0 };
    this.position = { x: 0, y: 0, z: 0 };
    this.scale = 1.0;
    this.isRotating = false;
    
    this.updateScene();
  }

  private updateScene() {
    this.sceneManager.setShape(this.selectedShape);
    this.sceneManager.setColor(this.color);
    this.sceneManager.setRotation(this.rotation.x, this.rotation.y, this.rotation.z);
    this.sceneManager.setPosition(this.position.x, this.position.y, this.position.z);
    this.sceneManager.setScale(this.scale);
    this.sceneManager.toggleRotation(this.isRotating);
  }
}
