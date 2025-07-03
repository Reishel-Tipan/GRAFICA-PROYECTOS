using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public class Circunferencia
    {
        private int xc, yc, radio;
        private List<Point> puntos;
        private List<Point> puntosAnimacion;
        private Timer timerAnimacion;
        private PictureBox pictureBox;
        private int indiceAnimacion;
        private bool animacionActiva;
        private Action actualizarUI;
        private int cellSize = 20; // Tamaño fijo de celda
        private int marginX, marginY;
        private int canvasSize;
        private int centerX, centerY;

        public bool AnimacionEnCurso => animacionActiva;

        public Circunferencia(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            puntos = new List<Point>();
            puntosAnimacion = new List<Point>();
            animacionActiva = false;
            indiceAnimacion = 0;
            
            // Configurar el temporizador para la animación
            timerAnimacion = new Timer();
            timerAnimacion.Interval = 10; // Ajusta la velocidad de la animación
            timerAnimacion.Tick += TimerAnimacion_Tick;
        }
        
        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (indiceAnimacion < puntosAnimacion.Count)
            {
                // Agregar el siguiente punto a la lista de puntos visibles
                puntos.Add(puntosAnimacion[indiceAnimacion]);
                indiceAnimacion++;
                
                // Actualizar la interfaz de usuario
                actualizarUI?.Invoke();
            }
            else
            {
                // La animación ha terminado
                DetenerAnimacion();
            }
        }
        
        public void IniciarAnimacion(Action actualizarUICallback)
        {
            if (animacionActiva) return;
            
            actualizarUI = actualizarUICallback;
            puntos.Clear();
            indiceAnimacion = 0;
            animacionActiva = true;
            
            // Calcular todos los puntos de la circunferencia para la animación
            CalcularPuntosAnimacion();
            
            // Iniciar el temporizador
            timerAnimacion.Start();
        }
        
        public void DetenerAnimacion()
        {
            if (!animacionActiva) return;
            
            timerAnimacion.Stop();
            animacionActiva = false;
        }
        
        private void CalcularPuntosAnimacion()
        {
            puntosAnimacion.Clear();
            
            int x = 0;
            int y = radio;
            int d = 3 - 2 * radio;
            
            // Función para agregar los 8 puntos de simetría
            Action<int, int> agregarPuntos = (xOffset, yOffset) =>
            {
                // Agregar los 8 puntos de simetría en orden
                puntosAnimacion.Add(new Point(xc + xOffset, yc + yOffset));
                if (xOffset != 0) puntosAnimacion.Add(new Point(xc - xOffset, yc + yOffset));
                if (yOffset != 0) puntosAnimacion.Add(new Point(xc + xOffset, yc - yOffset));
                if (xOffset != 0 && yOffset != 0) puntosAnimacion.Add(new Point(xc - xOffset, yc - yOffset));
                
                if (xOffset != yOffset)
                {
                    puntosAnimacion.Add(new Point(xc + yOffset, yc + xOffset));
                    if (yOffset != 0) puntosAnimacion.Add(new Point(xc - yOffset, yc + xOffset));
                    if (xOffset != 0) puntosAnimacion.Add(new Point(xc + yOffset, yc - xOffset));
                    if (xOffset != 0 && yOffset != 0) puntosAnimacion.Add(new Point(xc - yOffset, yc - xOffset));
                }
            };
            
            // Calcular todos los puntos de la circunferencia
            agregarPuntos(x, y);
            
            while (y >= x)
            {
                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    d = d + 4 * x + 6;
                }
                agregarPuntos(x, y);
            }
        }

        public bool ValidarDatos(string strX, string strY, string strRadio, PictureBox picBox = null)
        {
            try
            {
                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(strX) || string.IsNullOrWhiteSpace(strY) || string.IsNullOrWhiteSpace(strRadio))
                {
                    MessageBox.Show("Por favor ingrese valores para X, Y y el radio.", "Error de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                // Validar que los valores sean numéricos
                if (!int.TryParse(strX, out xc) || !int.TryParse(strY, out yc) || !int.TryParse(strRadio, out radio))
                {
                    MessageBox.Show("Por favor ingrese valores numéricos válidos.", "Error de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Validar que el radio sea positivo
                if (radio <= 0)
                {
                    MessageBox.Show("El radio debe ser un valor positivo.", "Error de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<Point> ObtenerPuntos()
        {
            return new List<Point>(puntos);
        }
        
        public void Limpiar()
        {
            puntos.Clear();
            puntosAnimacion.Clear();
            indiceAnimacion = 0;
        }

        public void DibujarCircunferencia(PictureBox picCanvas, bool animar = false)
        {
            // Configurar el área de dibujo
            Bitmap bmp = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics gBmp = Graphics.FromImage(bmp))
            using (Font font = new Font("Arial", 8))
            {
                gBmp.Clear(Color.White);
                Pen axisPen = new Pen(Color.Black, 2);
                Pen gridPen = new Pen(Color.LightGray, 1);

                // Calcular dimensiones basadas en el radio y el centro
                int minX = xc - radio - 5;  // Margen adicional para la cuadrícula
                int maxX = xc + radio + 5;
                int minY = yc - radio - 5;
                int maxY = yc + radio + 5;
                
                // Calcular el tamaño del área de dibujo
                int width = maxX - minX + 1;
                int height = maxY - minY + 1;
                int canvasWidth = width * cellSize;
                int canvasHeight = height * cellSize;
                
                // Centrar el área de dibujo
                marginX = (picCanvas.Width - canvasWidth) / 2;
                marginY = (picCanvas.Height - canvasHeight) / 2;
                
                // Funciones de conversión de coordenadas
                int ToScreenX(int worldX) => marginX + (worldX - minX) * cellSize;
                int ToScreenY(int worldY) => marginY + (maxY - worldY) * cellSize;

                // Dibujar cuadrícula
                for (int gridX = minX; gridX <= maxX; gridX++)
                {
                    int screenX = ToScreenX(gridX);
                    gBmp.DrawLine(gridPen, screenX, marginY, screenX, marginY + canvasHeight);
                    
                    // Etiquetas del eje X
                    if (gridX != 0)
                    {
                        gBmp.DrawLine(axisPen, screenX, ToScreenY(0) - 3, screenX, ToScreenY(0) + 3);
                        gBmp.DrawString(gridX.ToString(), font, Brushes.Black, screenX - 5, ToScreenY(0) + 5);
                    }
                }
                
                for (int gridY = minY; gridY <= maxY; gridY++)
                {
                    int screenY = ToScreenY(gridY);
                    gBmp.DrawLine(gridPen, marginX, screenY, marginX + canvasWidth, screenY);
                    
                    // Etiquetas del eje Y
                    if (gridY != 0)
                    {
                        gBmp.DrawLine(axisPen, ToScreenX(0) - 3, screenY, ToScreenX(0) + 3, screenY);
                        gBmp.DrawString(gridY.ToString(), font, Brushes.Black, ToScreenX(0) + 5, screenY - 8);
                    }
                }

                // Dibujar ejes
                int zeroX = ToScreenX(0);
                int zeroY = ToScreenY(0);
                gBmp.DrawLine(axisPen, marginX, zeroY, marginX + canvasWidth, zeroY); // Eje X
                gBmp.DrawLine(axisPen, zeroX, marginY, zeroX, marginY + canvasHeight); // Eje Y

                // Algoritmo de Bresenham para circunferencia
                int x = 0;
                int y = radio;
                int d = 3 - 2 * radio;
                
                puntos.Clear();
                
                // Función para dibujar un punto y sus simétricos
                void DibujarPunto(int x0, int y0)
                {
                    // Coordenadas del punto actual
                    int[,] puntosSimetricos = {
                        {x0, y0}, {x0, -y0}, {-x0, y0}, {-x0, -y0},
                        {y0, x0}, {y0, -x0}, {-y0, x0}, {-y0, -x0}
                    };
                    
                    // Dibujar los 8 puntos de simetría
                    for (int i = 0; i < 8; i++)
                    {
                        int px = xc + puntosSimetricos[i,0];
                        int py = yc + puntosSimetricos[i,1];
                        
                        // Convertir a coordenadas de pantalla
                        int screenX = ToScreenX(px);
                        int screenY = ToScreenY(py);
                        
                        // Dibujar el punto si está dentro de los límites
                        if (screenX >= marginX && screenX < marginX + canvasWidth && 
                            screenY >= marginY && screenY < marginY + canvasHeight)
                        {
                            gBmp.FillRectangle(Brushes.Blue, screenX, screenY, cellSize, cellSize);
                            puntos.Add(new Point(px, py));
                            
                            if (animar)
                            {
                                picCanvas.Image = (Bitmap)bmp.Clone();
                                picCanvas.Refresh();
                                System.Threading.Thread.Sleep(5);
                            }
                        }
                    }
                }

                // Dibujar los puntos iniciales
                DibujarPunto(x, y);

                // Algoritmo de Bresenham para circunferencia
                while (y >= x)
                {
                    x++;

                    // El punto medio está dentro o sobre el perímetro
                    if (d > 0)
                    {
                        y--;
                        d = d + 4 * (x - y) + 10;
                    }
                    else
                    {
                        d = d + 4 * x + 6;
                    }
                    DibujarPunto(x, y);
                }

                // Dibujar el centro
                int centerScreenX = ToScreenX(xc);
                int centerScreenY = ToScreenY(yc);
                gBmp.FillRectangle(Brushes.Red, centerScreenX, centerScreenY, cellSize, cellSize);
                
                // Mostrar coordenadas del centro
                string centerText = $"({xc},{yc})";
                gBmp.DrawString(centerText, font, Brushes.Black, centerScreenX + cellSize, centerScreenY - 15);

                // Establecer la imagen final
                picCanvas.Image = (Bitmap)bmp.Clone();
            }
        }
    }
}
