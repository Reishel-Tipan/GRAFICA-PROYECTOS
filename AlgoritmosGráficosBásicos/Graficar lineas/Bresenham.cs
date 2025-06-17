using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public class Bresenham
    {
        private readonly PictureBox pictureBox;
        private int x1, y1, x2, y2;
        private readonly List<Point> puntos;
        private readonly System.Windows.Forms.Timer animationTimer;
        private int animationIndex;
        private List<Point> animationPoints;
        private int cellSize = 40; // Tamaño de celda (aumentado para mejor visibilidad)
        private const int MaxCellSize = 60; // Tamaño máximo de celda
        private const int MinCellSize = 20; // Tamaño mínimo de celda
        private const int AnimationDelay = 50; // Retraso entre animaciones en milisegundos
        private readonly Color lineColor = Color.LimeGreen;
        private bool animationComplete = false;

        public Bresenham(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            puntos = new List<Point>();
            animationPoints = new List<Point>();
            
            // Configurar el temporizador de animación
            animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 50 // Velocidad de la animación (milisegundos)
            };
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public void SetPoints(int x1, int y1, int x2, int y2)
        {
            // Asegurar que los puntos sean positivos
            this.x1 = Math.Max(0, x1);
            this.y1 = Math.Max(0, y1);
            this.x2 = Math.Max(0, x2);
            this.y2 = Math.Max(0, y2);
            
            // Reiniciar el estado de la animación
            animationComplete = false;
            
            // Calcular los puntos de la línea usando el algoritmo de Bresenham
            CalculateLinePoints();
            
            // Iniciar la animación
            animationIndex = 0;
            puntos.Clear();
            animationTimer.Start();
        }

        private void CalculateLinePoints()
        {
            animationPoints.Clear();
            
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int err = dx - dy;
            
            int x = x1;
            int y = y1;
            
            while (true)
            {
                animationPoints.Add(new Point(x, y));
                
                if (x == x2 && y == y2) break;
                
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationIndex >= animationPoints.Count)
            {
                if (!animationComplete)
                {
                    animationComplete = true;
                    // Forzar un redibujado para mostrar la línea negra
                    DrawGridAndLine();
                }
                animationTimer.Stop();
                return;
            }
            
            // Asegurarse de no duplicar puntos
            if (!puntos.Contains(animationPoints[animationIndex]))
            {
                puntos.Add(animationPoints[animationIndex]);
            }
            animationIndex++;
            DrawGridAndLine();
            
            // Disparar evento cuando se actualicen los puntos
            PuntosActualizados?.Invoke(this, EventArgs.Empty);
        }

        private void DrawGridAndLine()
        {
            if (pictureBox.Image == null || 
                pictureBox.Width != pictureBox.Image.Width || 
                pictureBox.Height != pictureBox.Image.Height)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }

            using (var g = Graphics.FromImage(pictureBox.Image))
            using (var pointBrush = new SolidBrush(lineColor))
            {
                g.Clear(Color.White);
                
                // Calcular el área de dibujo
                int maxX = Math.Max(x1, x2) + 2; // +2 para margen
                int maxY = Math.Max(y1, y2) + 2;
                
                // Calcular el tamaño de celda basado en el tamaño del área de dibujo y los valores máximos
                int availableWidth = pictureBox.Width - 80; // Margen horizontal reducido
                int availableHeight = pictureBox.Height - 80; // Margen vertical reducido
                
                // Calcular el tamaño de celda máximo posible
                int maxCellWidth = maxX > 0 ? availableWidth / (maxX + 2) : MaxCellSize; // +2 para margen
                int maxCellHeight = maxY > 0 ? availableHeight / (maxY + 2) : MaxCellSize; // +2 para margen
                
                // Usar el menor de los dos tamaños para mantener la relación de aspecto
                cellSize = Math.Min(maxCellWidth, maxCellHeight);
                
                // Asegurar que el tamaño esté dentro de los límites
                cellSize = Math.Max(MinCellSize, Math.Min(MaxCellSize, cellSize));
                
                // Ajustar el tamaño de celda para que la cuadrícula quepa en la pantalla
                while ((maxX + 2) * cellSize > pictureBox.Width - 40 || 
                       (maxY + 2) * cellSize > pictureBox.Height - 40)
                {
                    cellSize--;
                    if (cellSize <= MinCellSize) break;
                }
                
                // Calcular el tamaño del área de dibujo con márgenes
                int gridWidth = (maxX + 1) * cellSize;
                int gridHeight = (maxY + 1) * cellSize;
                
                // Calcular los márgenes para centrar la cuadrícula
                int marginX = (pictureBox.Width - gridWidth) / 2;
                int marginY = (pictureBox.Height - gridHeight) / 2;
                
                // Dibujar la cuadrícula
                using (var gridPen = new Pen(Color.LightGray))
                {
                    // Líneas verticales
                    for (int x = 0; x <= maxX; x++)
                    {
                        int xPos = marginX + x * cellSize;
                        g.DrawLine(gridPen, xPos, marginY, xPos, marginY + gridHeight);
                        
                        // Etiquetas del eje X (solo cada 5 unidades o si es el primero/último)
                        if (x == 0 || x == maxX || x % 5 == 0)
                        {
                            g.DrawString(x.ToString(), SystemFonts.DefaultFont, Brushes.Black, 
                                xPos + 2, marginY + gridHeight + 2);
                        }
                    }
                    
                    // Líneas horizontales
                    for (int y = 0; y <= maxY; y++)
                    {
                        int yPos = marginY + y * cellSize;
                        g.DrawLine(gridPen, marginX, yPos, marginX + gridWidth, yPos);
                        
                        // Etiquetas del eje Y (solo cada 5 unidades o si es el primero/último)
                        int value = maxY - y - 1;
                        if (value == 0 || y == maxY - 1 || value % 5 == 0)
                        {
                            g.DrawString(value.ToString(), SystemFonts.DefaultFont, Brushes.Black, 
                                marginX - 25, yPos + 2);
                        }
                    }
                }
                
                // Dibujar ejes
                using (var axisPen = new Pen(Color.Black, 2))
                {
                    // Eje X
                    g.DrawLine(axisPen, marginX, marginY + gridHeight, 
                              marginX + gridWidth, marginY + gridHeight);
                    
                    // Eje Y
                    g.DrawLine(axisPen, marginX, marginY, marginX, marginY + gridHeight);
                    
                    // Flecha del eje X
                    g.DrawLine(axisPen, marginX + gridWidth - 10, marginY + gridHeight - 5, 
                              marginX + gridWidth, marginY + gridHeight);
                    g.DrawLine(axisPen, marginX + gridWidth - 10, marginY + gridHeight + 5, 
                              marginX + gridWidth, marginY + gridHeight);
                    
                    // Flecha del eje Y
                    g.DrawLine(axisPen, marginX - 5, marginY + 10, marginX, marginY);
                    g.DrawLine(axisPen, marginX + 5, marginY + 10, marginX, marginY);
                }
                
                // Calcular coordenadas de los puntos de inicio y fin en píxeles
                // Coordenadas del centro del primer píxel
                float startCenterX = marginX + x1 * cellSize + cellSize / 2f;
                float startCenterY = marginY + (maxY - y1 - 1) * cellSize + cellSize / 2f;
                
                // Coordenadas del centro del último píxel
                float endCenterX = marginX + x2 * cellSize + cellSize / 2f;
                float endCenterY = marginY + (maxY - y2 - 1) * cellSize + cellSize / 2f;
                
                // Calcular el vector de dirección de la línea
                float dx = endCenterX - startCenterX;
                float dy = endCenterY - startCenterY;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                
                // Normalizar el vector de dirección
                float dirX = dx / length;
                float dirY = dy / length;
                
                // Calcular el radio efectivo para llegar al borde del píxel (hipotenusa de medio píxel)
                float pixelRadius = (float)(cellSize / 2.0 * Math.Sqrt(2));
                
                // Calcular puntos de inicio y fin en el borde de los píxeles
                float startX = startCenterX - dirX * pixelRadius;
                float startY = startCenterY - dirY * pixelRadius;
                float endX = endCenterX + dirX * pixelRadius;
                float endY = endCenterY + dirY * pixelRadius;
                
                // Dibujar todos los píxeles de la línea en lila primero (en una capa inferior)
                foreach (var punto in puntos)
                {
                    int x = marginX + punto.X * cellSize;
                    int y = marginY + (maxY - punto.Y - 1) * cellSize;
                    g.FillRectangle(pointBrush, x, y, cellSize, cellSize);
                }
                
                // Dibujar el primer píxel (mismo color lila que los demás)
                int startPixelX = marginX + x1 * cellSize;
                int startPixelY = marginY + (maxY - y1 - 1) * cellSize;
                g.FillRectangle(pointBrush, startPixelX, startPixelY, cellSize, cellSize);
                
                // Dibujar el último píxel (mismo color lila que los demás)
                int endPixelX = marginX + x2 * cellSize;
                int endPixelY = marginY + (maxY - y2 - 1) * cellSize;
                g.FillRectangle(pointBrush, endPixelX, endPixelY, cellSize, cellSize);
                
                // Dibujar la línea negra al final (en la capa superior)
                if (animationComplete)
                {
                    using (var blackPen = new Pen(Color.Black, 3)) // Línea más gruesa
                    {
                        // Asegurar que la línea sea nítida
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        
                        // Dibujar la línea negra desde el borde del primer píxel hasta el borde del último píxel
                        g.DrawLine(blackPen, startX, startY, endX, endY);
                        
                        // Restaurar el modo de suavizado
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    }
                }
            }
            
            pictureBox.Invalidate();
        }
        
        public void PlotShape(PictureBox picCanvas)
        {
            // Método para compatibilidad
            DrawGridAndLine();
        }
        
        public void DibujarLinea(PictureBox picCanvas)
        {
            // Método para compatibilidad
            PlotShape(picCanvas);
        }
        
        // Evento que se dispara cuando se actualizan los puntos
        public event EventHandler PuntosActualizados;
        
        // Propiedad para acceder a los puntos desde el formulario
        public List<Point> PuntosDibujados => new List<Point>(puntos);
        
        // Método para obtener los puntos ordenados
        public List<Point> ObtenerPuntosOrdenados()
        {
            return puntos.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        }
    }
}
