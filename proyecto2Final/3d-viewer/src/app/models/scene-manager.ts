import * as THREE from 'three';
import { OrbitControls } from 'three-stdlib';
import { Injectable, OnDestroy } from '@angular/core';

export type ShapeType = 'cube' | 'sphere' | 'cone' | 'cylinder' | 'pyramid';

@Injectable({
  providedIn: 'root'
})
export class SceneManager implements OnDestroy {
  private scene: THREE.Scene = new THREE.Scene();
  private camera: THREE.PerspectiveCamera = new THREE.PerspectiveCamera();
  private renderer: THREE.WebGLRenderer = new THREE.WebGLRenderer();
  private controls!: OrbitControls;
  private animateId: number = 0;
  private currentShape: THREE.Mesh | THREE.Group | null = null;
  private shapes: { [key in ShapeType]?: THREE.Mesh | THREE.Group } = {};
  // Velocidades de rotación por eje (en radianes por frame)
  private rotationSpeed = { x: 0.02, y: 0.03, z: 0.01 };
  private isAutoRotating = false;

  // Propiedades para el fondo de galaxia
  private stars: THREE.Points | null = null;
  private starsGeometry: THREE.BufferGeometry | null = null;
  private starsMaterial: THREE.PointsMaterial | null = null;

  constructor(private canvas: HTMLCanvasElement) {
    this.init();
  }

  ngOnDestroy() {
    // Limpiar recursos
    cancelAnimationFrame(this.animateId);
    this.renderer.dispose();
    
    // Eliminar todos los objetos de la escena
    while (this.scene.children.length > 0) { 
      const object = this.scene.children[0];
      if (object instanceof THREE.Mesh) {
        object.geometry.dispose();
        if (Array.isArray(object.material)) {
          object.material.forEach(material => material.dispose());
        } else {
          object.material.dispose();
        }
      }
      this.scene.remove(object);
    }
  }

  private init() {
    this.setupScene();
    this.setupCamera();
    this.setupRenderer();
    this.setupLights();
    this.setupControls();
    this.createShapes();
    this.animate(0);
  }

  private createGalaxy() {
    // Crear geometría para las estrellas
    this.starsGeometry = new THREE.BufferGeometry();
    const vertices = [];
    const colors = [];
    
    // Colores para las estrellas (tonos fríos)
    const starColors = [
      0x4f46e5, 0x6366f1, 0x818cf8, 0xa5b4fc, 
      0xc7d2fe, 0xe0e7ff, 0x4f46e5, 0x7c3aed
    ];
    
    // Crear 5000 estrellas
    for (let i = 0; i < 5000; i++) {
      // Posición aleatoria en una esfera
      const vertex = new THREE.Vector3();
      vertex.x = Math.random() * 2 - 1;
      vertex.y = Math.random() * 2 - 1;
      vertex.z = Math.random() * 2 - 1;
      vertex.normalize();
      
      // Multiplicar por un radio aleatorio para dispersarlas
      const distance = 100 + Math.random() * 400; // Entre 100 y 500 unidades
      vertex.multiplyScalar(distance);
      
      // Agregar un poco de ruido a la posición
      vertex.x += (Math.random() - 0.5) * 20;
      vertex.y += (Math.random() - 0.5) * 20;
      vertex.z += (Math.random() - 0.5) * 20;
      
      vertices.push(vertex.x, vertex.y, vertex.z);
      
      // Color aleatorio de la paleta
      const color = new THREE.Color(starColors[Math.floor(Math.random() * starColors.length)]);
      colors.push(color.r, color.g, color.b);
    }
    
    // Configurar geometría
    this.starsGeometry.setAttribute('position', new THREE.Float32BufferAttribute(vertices, 3));
    this.starsGeometry.setAttribute('color', new THREE.Float32BufferAttribute(colors, 3));
    
    // Material de las estrellas
    this.starsMaterial = new THREE.PointsMaterial({
      size: 1.5,
      sizeAttenuation: true,
      vertexColors: true,
      transparent: true,
      opacity: 0.8,
      blending: THREE.AdditiveBlending
    });
    
    // Crear el sistema de partículas
    this.stars = new THREE.Points(this.starsGeometry, this.starsMaterial);
    this.scene.add(this.stars);
    
    // Crear nebulosa
    this.createNebula();
  }
  
  private createNebula() {
    // Crear geometría para la nebulosa
    const nebulaGeometry = new THREE.BufferGeometry();
    const particleCount = 1000;
    const positions = [];
    const sizes = [];
    const colors = [];
    
    // Colores para la nebulosa (tonos púrpuras y azules)
    const nebulaColors = [
      {r: 0.3, g: 0.1, b: 0.5},  // Púrpura oscuro
      {r: 0.5, g: 0.2, b: 0.7},  // Púrpura
      {r: 0.2, g: 0.1, b: 0.8},  // Azul oscuro
      {r: 0.1, g: 0.3, b: 0.9}   // Azul
    ];
    
    // Crear partículas para la nebulosa
    for (let i = 0; i < particleCount; i++) {
      // Posición en una esfera
      const radius = 150 + Math.random() * 100;
      const theta = Math.random() * Math.PI * 2;
      const phi = Math.acos(2 * Math.random() - 1);
      
      const x = radius * Math.sin(phi) * Math.cos(theta);
      const y = radius * Math.sin(phi) * Math.sin(theta) * 0.5; // Aplanar en Y
      const z = radius * Math.cos(phi);
      
      positions.push(x, y, z);
      
      // Tamaño aleatorio
      sizes.push(10 + Math.random() * 40);
      
      // Color aleatorio de la paleta
      const color = nebulaColors[Math.floor(Math.random() * nebulaColors.length)];
      colors.push(color.r, color.g, color.b);
    }
    
    // Configurar geometría
    nebulaGeometry.setAttribute('position', new THREE.Float32BufferAttribute(positions, 3));
    nebulaGeometry.setAttribute('color', new THREE.Float32BufferAttribute(colors, 3));
    nebulaGeometry.setAttribute('size', new THREE.Float32BufferAttribute(sizes, 1));
    
    // Material de la nebulosa
    const nebulaMaterial = new THREE.PointsMaterial({
      size: 1,
      sizeAttenuation: true,
      vertexColors: true,
      transparent: true,
      opacity: 0.1,
      blending: THREE.AdditiveBlending,
      depthWrite: false
    });
    
    // Crear la nebulosa
    const nebula = new THREE.Points(nebulaGeometry, nebulaMaterial);
    this.scene.add(nebula);
  }
  
  private setupScene() {
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0x0a0a1a); // Fondo oscuro profundo
    
    // Crear la galaxia de fondo
    this.createGalaxy();
  }

  private setupCamera() {
    this.camera = new THREE.PerspectiveCamera(
      45, // campo de visión
      this.canvas.clientWidth / this.canvas.clientHeight, // relación de aspecto
      0.1, // plano cercano
      1000 // plano lejano
    );
    // Posicionar la cámara mirando al centro de la escena
    this.camera.position.set(0, 0, 5);
    this.camera.lookAt(0, 0, 0);
  }

  private setupRenderer() {
    this.renderer = new THREE.WebGLRenderer({
      canvas: this.canvas,
      antialias: true,
      alpha: true
    });
    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);
    this.renderer.setPixelRatio(window.devicePixelRatio);
    
    // Manejar redimensionamiento
    window.addEventListener('resize', this.onWindowResize.bind(this));
  }

  private setupLights() {
    // Luz ambiental más brillante con un tono azulado
    const ambientLight = new THREE.AmbientLight(0x6060ff, 0.5);
    this.scene.add(ambientLight);

    // Luces principales más intensas
    const light1 = new THREE.DirectionalLight(0xff9f50, 1.5); // Más cálida y brillante
    light1.position.set(5, 10, 7);
    light1.castShadow = true;
    light1.shadow.mapSize.width = 2048;
    light1.shadow.mapSize.height = 2048;
    light1.shadow.camera.near = 0.5;
    light1.shadow.camera.far = 100;
    this.scene.add(light1);

    // Luz de relleno más intensa
    const light2 = new THREE.DirectionalLight(0x6db8ff, 1.2); // Azul más brillante
    light2.position.set(-5, 3, -5);
    this.scene.add(light2);

    // Luz de acento más intensa
    const light3 = new THREE.PointLight(0xffe44d, 3, 15); // Más brillante y con mayor alcance
    light3.position.set(3, 5, -3);
    this.scene.add(light3);

    // Luz ambiental más brillante
    const hemiLight = new THREE.HemisphereLight(0xffffff, 0x6666ff, 0.8); // Añadido tono azulado
    hemiLight.position.set(0, 20, 0);
    this.scene.add(hemiLight);
    
    // Luz adicional desde abajo para reducir sombras oscuras
    const fillLight = new THREE.DirectionalLight(0x4444ff, 0.5);
    fillLight.position.set(0, -10, 0);
    this.scene.add(fillLight);
  }

  private setupControls() {
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableDamping = true;
  }

  private createMaterial(color: number, isWireframe: boolean = false): THREE.Material {
    // Material mejorado con colores más intensos y brillantes
    return new THREE.MeshPhysicalMaterial({
      color: color,
      metalness: 0.5, // Reducido para colores más puros
      roughness: 0.1, // Más suave para más brillo
      clearcoat: 1.0, // Aumentado para más brillo
      clearcoatRoughness: 0.1, // Más suave para reflejos más nítidos
      emissive: color, // Usar el mismo color para emisión
      emissiveIntensity: 0.2, // Añadir un poco de brillo propio
      ior: 1.8, // Índice de refracción más alto para más brillo
      transmission: 0.0,
      specularIntensity: 1.2, // Aumentado para reflejos más intensos
      wireframe: isWireframe,
      flatShading: false,
      side: THREE.DoubleSide,
      envMapIntensity: 1.5, // Aumentado para reflejos más brillantes
      sheen: 0.5, // Añadir brillo de tela
      sheenRoughness: 0.3 // Suavizar el brillo de tela
    });
  }

  private createShapes() {
    // Crear cubo con bordes resaltados
    const cubeGeometry = new THREE.BoxGeometry(1, 1, 1, 10, 10, 10);
    const cubeMaterial = this.createMaterial(0x4f46e5);
    this.shapes['cube'] = new THREE.Mesh(cubeGeometry, cubeMaterial);
    this.shapes['cube'].visible = false; // Ocultar por defecto
    
    // Añadir bordes resaltados al cubo
    const cubeEdges = new THREE.EdgesGeometry(cubeGeometry);
    const cubeEdgesMaterial = new THREE.LineBasicMaterial({ 
      color: 0xffffff, 
      linewidth: 2,
      transparent: true,
      opacity: 0.3
    });
    const cubeWireframe = new THREE.LineSegments(cubeEdges, cubeEdgesMaterial);
    this.shapes['cube'].add(cubeWireframe);
    
    // Crear esfera con material especial
    const sphereGeometry = new THREE.SphereGeometry(0.6, 24, 24); // Reducir segmentos para mejor rendimiento
    
    // Material principal de la esfera
    const sphereMaterial = this.createMaterial(0x6366f1);
    
    // Crear la esfera principal
    const sphereGroup = new THREE.Group();
    const sphereMesh = new THREE.Mesh(sphereGeometry, sphereMaterial);
    
    // Crear la malla de líneas para la esfera
    const edges = new THREE.EdgesGeometry(sphereGeometry);
    const lineMaterial = new THREE.LineBasicMaterial({ 
      color: 0xffffff, 
      transparent: true,
      opacity: 0.3,
      linewidth: 1
    });
    const wireframe = new THREE.LineSegments(edges, lineMaterial);
    
    // Añadir tanto la esfera como la malla al grupo
    sphereGroup.add(sphereMesh);
    sphereGroup.add(wireframe);
    this.shapes['sphere'] = sphereGroup;
    this.shapes['sphere'].visible = false; // Ocultar por defecto
    
    // Crear cono con material metálico
    const coneGeometry = new THREE.ConeGeometry(0.6, 1.2, 64, 1, true);
    const coneMaterial = this.createMaterial(0x8b5cf6);
    this.shapes['cone'] = new THREE.Mesh(coneGeometry, coneMaterial);
    this.shapes['cone'].visible = false; // Ocultar por defecto
    
    // Crear cilindro con bordes suaves
    const cylinderGeometry = new THREE.CylinderGeometry(0.6, 0.6, 1.2, 64, 1, false);
    const cylinderMaterial = this.createMaterial(0x6366f1);
    this.shapes['cylinder'] = new THREE.Mesh(cylinderGeometry, cylinderMaterial);
    this.shapes['cylinder'].visible = false; // Ocultar por defecto
    
    // Crear pirámide con bordes definidos
    const pyramidGeometry = new THREE.ConeGeometry(0.85, 1.7, 4, 1, false);
    const pyramidMaterial = this.createMaterial(0x7c3aed);
    this.shapes['pyramid'] = new THREE.Mesh(pyramidGeometry, pyramidMaterial);
    this.shapes['pyramid'].visible = false; // Ocultar por defecto
    
    // Añadir todas las formas a la escena
    Object.values(this.shapes).forEach(shape => this.scene.add(shape));
    
    // Establecer el cubo como forma inicial
    this.setShape('cube');
  }

  public setShape(shapeType: ShapeType) {
    // Asegurarse de que todas las figuras estén ocultas primero
    Object.values(this.shapes).forEach(shape => {
      if (shape) shape.visible = false;
    });
    
    // Establecer y mostrar la nueva figura
    const newShape = this.shapes[shapeType];
    if (newShape) {
      this.currentShape = newShape;
      this.currentShape.visible = true;
      
      // Ajustar la cámara para la nueva figura
      this.controls.reset();
      this.camera.position.z = 5;
      this.controls.update();
    }
  }

  private animate(time: number) {
    this.animateId = requestAnimationFrame((t) => this.animate(t));
    
    // Rotación automática suave
    if (this.isAutoRotating && this.currentShape) {
      // Aplicamos la rotación en los ejes correspondientes
      // Usamos quaternions para evitar bloqueo de cardán
      const rotation = new THREE.Quaternion();
      rotation.setFromEuler(new THREE.Euler(
        this.rotationSpeed.y * 0.5, // Rotación más lenta para la figura
        this.rotationSpeed.x * 0.5,
        0,
        'XYZ'
      ));
      this.currentShape.quaternion.multiplyQuaternions(rotation, this.currentShape.quaternion);
    }
    
    // Rotar la galaxia de fondo (más lento que la figura)
    if (this.stars) {
      this.stars.rotation.x += 0.0001;
      this.stars.rotation.y += 0.0002;
    }
    
    // Actualizar controles y renderizar
    this.controls.update();
    this.renderer.render(this.scene, this.camera);
  }

  public toggleRotation(enable: boolean) {
    this.isAutoRotating = enable;
    
    // Si se está habilitando la rotación, forzar un renderizado
    if (enable) {
      this.animate(performance.now());
    }
  }

  public onWindowResize() {
    const width = this.canvas.clientWidth;
    const height = this.canvas.clientHeight;
    
    this.camera.aspect = width / height;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(width, height);
  }
  
  public setColor(color: string) {
    if (!this.currentShape) return;
    
    const hexColor = new THREE.Color(color);
    
    // Crear variaciones de color para efectos
    const lighterColor = hexColor.clone().offsetHSL(0, 0, 0.2);
    const darkerColor = hexColor.clone().offsetHSL(0, 0, -0.2);
    
    // Si es un grupo (como la esfera con malla)
    if (this.currentShape instanceof THREE.Group) {
      this.currentShape.traverse((child) => {
        if (child instanceof THREE.Mesh) {
          const meshMat = child.material as THREE.MeshPhysicalMaterial;
          if (meshMat) {
            meshMat.color.set(hexColor);
            meshMat.emissive.setHex(hexColor.getHex()).multiplyScalar(0.1);
          }
        } else if (child instanceof THREE.LineSegments) {
          const lineMat = child.material as THREE.LineBasicMaterial;
          if (lineMat) {
            // Mantener las líneas en un color de contraste
            const brightness = (hexColor.r * 299 + hexColor.g * 587 + hexColor.b * 114) / 1000;
            lineMat.color.set(brightness > 0.5 ? 0x000000 : 0xffffff);
          }
        }
      });
      return;
    }
    
    // Para el resto de formas
    const material = this.currentShape.material;
    
    if (Array.isArray(material)) {
      // Para materiales múltiples (como la esfera con gradiente)
      material.forEach((mat, index) => {
        if (mat instanceof THREE.Material) {
          const meshMat = mat as THREE.MeshPhysicalMaterial;
          meshMat.color.set(index === 0 ? hexColor : 
                           index === 1 ? lighterColor : darkerColor);
          meshMat.emissive.setHex(hexColor.getHex()).multiplyScalar(0.1);
        }
      });
    } else if (material instanceof THREE.Material) {
      // Para materiales simples
      const meshMat = material as THREE.MeshPhysicalMaterial;
      meshMat.color.set(hexColor);
      
      // Añadir un efecto de resplandor sutil
      meshMat.emissive.setHex(hexColor.getHex()).multiplyScalar(0.1);
      
      // Actualizar el material para forzar la actualización
      this.currentShape.material = meshMat;
    }
    
    // Actualizar bordes resaltados si existen
    if (this.currentShape.children.length > 0) {
      const wireframe = this.currentShape.children[0] as THREE.LineSegments;
      if (wireframe && wireframe.material) {
        const wireMat = wireframe.material as THREE.LineBasicMaterial;
        wireMat.color.set(0xffffff); // Mantener bordes blancos para contraste
      }
    }
  }
  
  public setRotation(x: number, y: number, z: number) {
    if (this.currentShape) {
      // Convertir ángulos de grados a radianes e intercambiamos X e Y
      const radX = THREE.MathUtils.degToRad(y); // Intercambiamos X e Y
      const radY = THREE.MathUtils.degToRad(x); // Intercambiamos Y y X
      const radZ = THREE.MathUtils.degToRad(z);
      
      // Aplicar rotación usando quaternions para evitar bloqueo de cardán
      this.currentShape.setRotationFromEuler(new THREE.Euler(radX, radY, radZ, 'XYZ'));
    }
  }
  
  public setPosition(x: number, y: number, z: number) {
    if (this.currentShape) {
      this.currentShape.position.set(x, y, z);
    }
  }
  
  public setScale(scale: number) {
    if (this.currentShape) {
      this.currentShape.scale.set(scale, scale, scale);
    }
  }
}