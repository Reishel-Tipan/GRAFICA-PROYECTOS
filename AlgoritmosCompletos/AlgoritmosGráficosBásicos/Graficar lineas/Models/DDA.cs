using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class DDA
    {
        private readonly PictureBox pictureBox;
        private int x1, y1, x2, y2;
        private List<Point> puntos = new List<Point>();
        private List<Point> animationPoints = new List<Point>();
        private System.Windows.Forms.Timer animationTimer;
        private int animationIndex;
        private int cellSize = 40; // Tamaño de celda (aumentado para mejor visibilidad)
        private const int MaxCellSize = 60; // Tamaño máximo de celda
        private const int MinCellSize = 20; // Tamaño mínimo de celda
        private const int AnimationDelay = 50; // Retraso entre animaciones en milisegundos
        private readonly Color lineColor = Color.LimeGreen;
        private bool animationComplete = false;
        public event EventHandler PuntosActualizados;
        
        // Propiedad para acceder a los puntos dibujados
        public List<Point> PuntosDibujados => new List<Point>(puntos);

        public DDA(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            
            // Configurar el temporizador de animación
            animationTimer = new System.Windows.Forms.Timer
            {
                Interval = AnimationDelay
            };
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public bool ReadData(TextBox x11, TextBox x22, TextBox y11, TextBox y22)
        {
            try
            {
                // Verificar que los valores ingresados sean enteros y mayores o iguales que 0
                x1 = int.Parse(x11.Text);
                x2 = int.Parse(x22.Text);
                y1 = int.Parse(y11.Text);
                y2 = int.Parse(y22.Text);

                // Comprobar que los valores son mayores o iguales que 0
                if (x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0)
                {
                    MessageBox.Show("Los valores no pueden ser negativos", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                // Calcular los puntos de la línea usando DDA
                CalculateLinePoints();
                
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese solo números enteros", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los datos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        private void CalculateLinePoints()
        {
            puntos.Clear();
            animationPoints.Clear();
            
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            
            // Si la línea es un solo punto
            if (steps == 0)
            {
                puntos.Add(new Point(x1, y1));
                animationPoints.Add(new Point(x1, y1));
                return;
            }
            
            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;
            
            float x = x1;
            float y = y1;
            
            for (int i = 0; i <= steps; i++)
            {
                int xRounded = (int)Math.Round(x);
                int yRounded = (int)Math.Round(y);
                
                // Solo agregar el punto si es diferente al anterior
                if (puntos.Count == 0 || puntos.Last().X != xRounded || puntos.Last().Y != yRounded)
                {
                    puntos.Add(new Point(xRounded, yRounded));
                }
                
                animationPoints.Add(new Point(xRounded, yRounded));
                x += xIncrement;
                y += yIncrement;
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
            var currentPoint = animationPoints[animationIndex];
            if (puntos.Count == 0 || puntos.Last().X != currentPoint.X || puntos.Last().Y != currentPoint.Y)
            {
                puntos.Add(currentPoint);
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
                
                // Dibujar cuadrícula
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
                
                // Dibujar ejes con flechas
                using (var axisPen = new Pen(Color.Black, 2))
                {
                    // Eje X
                    g.DrawLine(axisPen, marginX, marginY + gridHeight, marginX + gridWidth, marginY + gridHeight);
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
                
                // Dibujar todos los píxeles de la línea en lila
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
            // No asignar a pictureBox si es de solo lectura
            // En su lugar, usar directamente el parámetro
            
            // Iniciar animación
            animationIndex = 0;
            animationComplete = false;
            puntos.Clear();
            
            // Si no hay puntos para animar, dibujar directamente
            if (animationPoints.Count == 0)
            {
                DrawGridAndLine();
            }
            else
            {
                animationTimer.Start();
            }
            
            // No es necesario llamar a DrawGridAndLine() de nuevo aquí
        }


        public void DibujarMenos(PictureBox picCanvas)
        {
            // Este método ya no es necesario con la nueva implementación
            // Se mantiene por compatibilidad
            PlotShape(picCanvas);
        }
        
        // Método para obtener los puntos ordenados
        public List<Point> ObtenerPuntosOrdenados()
        {
            return puntos.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        }

            // Método para dibujar la línea usando el algoritmo DDA (Digital Differential Analyzer)
        private void DrawDDALine(Graphics g, int x1, int y1, int x2, int y2, int cellSize, int marginX, int marginY, int maxY)
        {
            // Calcular diferencias y pasos
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            // Si es un solo punto
            if (steps == 0)
            {
                int x = marginX + x1 * cellSize;
                int y = marginY + (maxY - y1 - 1) * cellSize;
                g.FillRectangle(Brushes.LimeGreen, x, y, cellSize, cellSize);
                return;
            }

            // Calcular incrementos
            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            // Punto actual
            float currentX = x1;
            float currentY = y1;

            // Dibujar todos los puntos de la línea
            for (int i = 0; i <= steps; i++)
            {
                int xRounded = (int)Math.Round(currentX);
                int yRounded = (int)Math.Round(currentY);
                
                int pixelX = marginX + xRounded * cellSize;
                int pixelY = marginY + (maxY - yRounded - 1) * cellSize;
                
                g.FillRectangle(Brushes.LimeGreen, pixelX, pixelY, cellSize, cellSize);
                
                currentX += xIncrement;
                currentY += yIncrement;
            }
        }
    }
}
