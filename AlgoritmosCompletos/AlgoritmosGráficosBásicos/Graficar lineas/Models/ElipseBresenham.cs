using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class ElipseBresenham
    {
        private PictureBox pictureBox;
        private Bitmap bmp;
        private Graphics g;
        private int centerX, centerY;
        private int a, b; // a = semi-eje mayor, b = semi-eje menor
        private Color color;
        private List<Point> puntos;

        public List<Point> Puntos => puntos;

        public ElipseBresenham(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            color = Color.Coral;
            puntos = new List<Point>();
        }

        public void ConfigurarElipse(int centroX, int centroY, int radioMayor, int radioMenor)
        {
            centerX = centroX;
            centerY = centroY;
            a = radioMayor;
            b = radioMenor;
            puntos.Clear();
        }

        public void Dibujar()
        {
            if (a <= 0 || b <= 0) return;

            // Limpiar el área de dibujo
            g.Clear(Color.White);
            DibujarEjes();
            DibujarCuadricula();
            
            // Forzar la actualización inicial del PictureBox
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            pictureBox.Image = (Bitmap)bmp.Clone();
            pictureBox.Refresh();
            Application.DoEvents();

            // Aplicar el algoritmo de Bresenham para la elipse con animación
            DibujarElipseBresenham();
        }

        private void DibujarElipseBresenham()
        {
            int x = 0;
            int y = b;
            double d1 = (b * b) - (a * a * b) + (0.25 * a * a);
            int dx = 2 * b * b * x;
            int dy = 2 * a * a * y;

            // Región 1
            while (dx < dy)
            {
                DibujarPuntosSimetricos(x, y);
                x++;
                dx += 2 * b * b;

                if (d1 < 0)
                {
                    d1 += dx + (b * b);
                }
                else
                {
                    y--;
                    dy -= 2 * a * a;
                    d1 += dx - dy + (b * b);
                }
            }

            // Región 2
            double d2 = ((b * b) * ((x + 0.5) * (x + 0.5))) +
                       ((a * a) * ((y - 1) * (y - 1))) -
                       (a * a * b * b);

            while (y >= 0)
            {
                DibujarPuntosSimetricos(x, y);
                y--;
                dy -= 2 * a * a;

                if (d2 > 0)
                {
                    d2 += (a * a) - dy;
                }
                else
                {
                    x++;
                    dx += 2 * b * b;
                    d2 += dx - dy + (a * a);
                }
            }
        }

        private void DibujarPuntosSimetricos(int x, int y)
        {
            try
            {
                if (bmp == null) return;

                // Dibujar en los 4 cuadrantes
                int x1 = centerX + x;
                int y1 = centerY + y;
                int x2 = centerX - x;
                int y2 = centerY - y;
                int x3 = centerX + x;
                int y3 = centerY - y;
                int x4 = centerX - x;
                int y4 = centerY + y;

                // Usar un pincel más grueso (2 píxeles)
                using (var g = Graphics.FromImage(bmp))
                {
                    // Suavizado para mejor calidad
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Dibujar puntos más grandes (círculos pequeños)
                    int pointSize = 3; // Tamaño del punto
                    Action<int, int> drawPoint = (px, py) =>
                    {
                        if (px >= 0 && px < bmp.Width && py >= 0 && py < bmp.Height)
                        {
                            g.FillEllipse(new SolidBrush(color), 
                                px - pointSize/2, py - pointSize/2, pointSize, pointSize);
                            puntos.Add(new Point(px, py));
                        }
                    };

                    // Dibujar los 4 puntos simétricos
                    drawPoint(x1, y1);
                    drawPoint(x2, y2);
                    drawPoint(x3, y3);
                    drawPoint(x4, y4);
                }

                // Actualizar la imagen en el PictureBox de forma segura
                UpdatePictureBoxSafe();
                
                // Pequeña pausa para la animación
                System.Threading.Thread.Sleep(5);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en DibujarPuntosSimetricos: {ex.Message}");
            }
        }

        private void UpdatePictureBoxSafe()
        {
            try
            {
                if (pictureBox == null || pictureBox.IsDisposed || bmp == null)
                    return;

                // Usar Invoke si es necesario
                if (pictureBox.InvokeRequired)
                {
                    pictureBox.BeginInvoke(new Action(UpdatePictureBoxSafe));
                    return;
                }

                // Crear una copia del bitmap actual
                using (var newImage = (Bitmap)bmp.Clone())
                {
                    // Guardar la imagen anterior
                    var oldImage = pictureBox.Image;
                    
                    // Asignar la nueva imagen
                    pictureBox.Image = (Bitmap)newImage.Clone();
                    
                    // Liberar la imagen anterior si existe
                    oldImage?.Dispose();
                    
                    // Forzar la actualización
                    pictureBox.Update();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en UpdatePictureBoxSafe: {ex.Message}");
            }
        }

        private void UpdatePictureBox()
        {
            if (pictureBox == null || pictureBox.IsDisposed || bmp == null)
                return;

            try
            {
                // Crear una copia del bitmap actual
                using (var newImage = (Bitmap)bmp.Clone())
                {
                    // Guardar la imagen anterior
                    var oldImage = pictureBox.Image;
                    
                    // Asignar la nueva imagen
                    pictureBox.Image = (Bitmap)newImage.Clone();
                    
                    // Liberar la imagen anterior si existe
                    oldImage?.Dispose();
                    
                    // Forzar la actualización
                    pictureBox.Update();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en UpdatePictureBox: {ex.Message}");
            }
        }

        private void DibujarEjes()
        {
            using (var pen = new Pen(Color.LightGray, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                // Obtener el centro de la imagen para los ejes
                int centerImgX = bmp.Width / 2;
                int centerImgY = bmp.Height / 2;
                
                // Eje X
                g.DrawLine(pen, 0, centerImgY, bmp.Width, centerImgY);
                // Eje Y
                g.DrawLine(pen, centerImgX, 0, centerImgX, bmp.Height);
            }
        }

        private void DibujarCuadricula()
        {
            Pen pen = new Pen(Color.LightGray, 1);
            int cellSize = 20; // Tamaño de celda de la cuadrícula

            // Líneas verticales
            for (int x = 0; x < pictureBox.Width; x += cellSize)
            {
                g.DrawLine(pen, x, 0, x, pictureBox.Height);
            }

            // Líneas horizontales
            for (int y = 0; y < pictureBox.Height; y += cellSize)
            {
                g.DrawLine(pen, 0, y, pictureBox.Width, y);
            }
        }

        public void Limpiar()
        {
            // Limpiar el bitmap
            g.Clear(Color.White);
            DibujarEjes();
            DibujarCuadricula();
            
            // Actualizar el PictureBox
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
            
            // Crear un nuevo bitmap limpio
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            DibujarEjes();
            DibujarCuadricula();
            
            // Limpiar la lista de puntos
            puntos.Clear();
            
            // Actualizar el PictureBox
            pictureBox.Image = (Bitmap)bmp.Clone();
        }
    }
}
