using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class Linea : IDisposable
    {
        #region IDisposable Implementation
        private Graphics g;
        private Pen pen;
        private bool disposed = false;

        public Linea(Graphics g, Pen pen)
        {
            this.g = g;
            this.pen = pen;
        }

        public void DrawShape(int index, int cX, int cY, int sX, int sY, int x, int y)
        {
            if (index == 5) g.DrawLine(pen, cX, cY, x, y);
        }

        public void DrawPreview(Graphics g, int index, int cX, int cY, int sX, int sY, int x, int y)
        {
            if (index == 5) g.DrawLine(pen, cX, cY, x, y);
        }

        public void DrawBezierCurve(List<PointF> points)
        {
            if (points == null || points.Count < 2) return;

            int resolution = 100;
            PointF prevPoint = points[0];

            for (int t = 1; t <= resolution; t++)
            {
                float l = t / (float)resolution;
                PointF currentPoint = CalculateBezierPoint(l, points);
                g.DrawLine(pen, prevPoint, currentPoint);
                prevPoint = currentPoint;
            }
        }

        private PointF CalculateBezierPoint(float t, List<PointF> points)
        {
            int n = points.Count - 1;
            PointF result = new PointF(0, 0);

            for (int i = 0; i <= n; i++)
            {
                float binomialCoefficient = BinomialCoefficient(n, i);
                float term = binomialCoefficient * (float)Math.Pow(1 - t, n - i) * (float)Math.Pow(t, i);
                result.X += term * points[i].X;
                result.Y += term * points[i].Y;
            }

            return result;
        }

        private float BinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;

            float result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= (n - (k - i)) / (float)i;
            }

            return result;
        }

        public async Task AnimarCurvaBezier(Graphics canvas, List<PointF> puntos, int numSegmentos, PictureBox pictuCanva, Bitmap bm)
        {
            if (puntos == null || puntos.Count < 2 || pictuCanva == null || bm == null) 
                return;

            Bitmap tempBitmap = new Bitmap(bm.Width, bm.Height);
            Graphics g = Graphics.FromImage(tempBitmap);
            Pen penConstruccion = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
            Pen penCurva = new Pen(pen.Color, pen.Width);
            List<PointF> puntosCurva = new List<PointF>();

            try
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                float tIncrement = 1f / numSegmentos;

                for (float t = 0; t <= 1; t += tIncrement)
                {
                    // Limpiar el bitmap temporal
                    g.Clear(Color.White);
                    
                    // Dibujar el paso actual de la animación
                    DibujarPasoAnimacion(g, puntos, t, penConstruccion, penCurva, ref puntosCurva);
                    
                    // Dibujar los puntos de control
                    foreach (var punto in puntos)
                    {
                        g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                    }
                    
                    // Actualizar la vista
                    if (pictuCanva.InvokeRequired)
                    {
                        await Task.Run(() => 
                        {
                            pictuCanva.Invoke(new Action(() => 
                            {
                                using (Graphics gDest = pictuCanva.CreateGraphics())
                                {
                                    gDest.DrawImageUnscaled(tempBitmap, 0, 0);
                                }
                            }));
                        });
                    }
                    else
                    {
                        using (Graphics gDest = pictuCanva.CreateGraphics())
                        {
                            gDest.DrawImageUnscaled(tempBitmap, 0, 0);
                        }
                    }
                    
                    // Pequeña pausa para la animación
                    await Task.Delay(10);
                }

                // Dibujar el resultado final en el bitmap principal
                using (Graphics finalG = Graphics.FromImage(bm))
                {
                    finalG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    finalG.Clear(Color.White);
                    
                    // Dibujar puntos de control
                    foreach (var punto in puntos)
                    {
                        finalG.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                    }
                    
                    // Dibujar la curva final
                    if (puntosCurva.Count > 1)
                    {
                        finalG.DrawCurve(penCurva, puntosCurva.ToArray());
                    }
                }

                // Actualizar el PictureBox con el resultado final
                if (pictuCanva.InvokeRequired)
                {
                    pictuCanva.Invoke(new Action(() => 
                    {
                        if (pictuCanva.Image != null)
                        {
                            var oldImage = pictuCanva.Image;
                            pictuCanva.Image = null;
                            oldImage.Dispose();
                        }
                        pictuCanva.Image = new Bitmap(bm);
                        pictuCanva.Refresh();
                    }));
                }
                else
                {
                    if (pictuCanva.Image != null)
                    {
                        var oldImage = pictuCanva.Image;
                        pictuCanva.Image = null;
                        oldImage.Dispose();
                    }
                    pictuCanva.Image = new Bitmap(bm);
                    pictuCanva.Refresh();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la animación: {ex.Message}");
            }
            finally
            {
                // Liberar recursos
                g.Dispose();
                tempBitmap.Dispose();
                penConstruccion.Dispose();
                penCurva.Dispose();
            }
        }

        private void DibujarPasoAnimacion(Graphics g, List<PointF> puntos, float t, Pen penConstruccion, Pen penCurva, ref List<PointF> puntosCurva)
        {
            // Copiar los puntos para la interpolación
            List<PointF> puntosActuales = new List<PointF>(puntos);
            List<PointF> siguientesPuntos = new List<PointF>();

            // Dibujar líneas de construcción
            while (puntosActuales.Count > 1)
            {
                siguientesPuntos.Clear();

                for (int i = 0; i < puntosActuales.Count - 1; i++)
                {
                    float x = (1 - t) * puntosActuales[i].X + t * puntosActuales[i + 1].X;
                    float y = (1 - t) * puntosActuales[i].Y + t * puntosActuales[i + 1].Y;
                    siguientesPuntos.Add(new PointF(x, y));
                    
                    if (puntosActuales.Count > 2) // Solo dibujar líneas de construcción si hay más de 2 puntos
                    {
                        g.DrawLine(penConstruccion, puntosActuales[i], puntosActuales[i + 1]);
                    }
                }

                // Dibujar puntos intermedios
                foreach (var punto in puntosActuales)
                {
                    g.FillEllipse(Brushes.Green, punto.X - 2, punto.Y - 2, 4, 4);
                }

                // Actualizar para la siguiente iteración
                var temp = puntosActuales;
                puntosActuales = siguientesPuntos;
                siguientesPuntos = temp;
            }

            // Dibujar el punto actual de la curva
            if (puntosActuales.Count > 0)
            {
                PointF puntoCurva = puntosActuales[0];
                puntosCurva.Add(puntoCurva);
                
                // Dibujar la curva hasta el momento
                if (puntosCurva.Count > 1)
                {
                    g.DrawLines(penCurva, puntosCurva.ToArray());
                }
                
                // Dibujar el punto actual
                g.FillEllipse(Brushes.Blue, puntoCurva.X - 3, puntoCurva.Y - 3, 6, 6);
            }
        }

        private void ActualizarVista(Bitmap tempBitmap, PictureBox pictuCanva)
        {
            using (Graphics gDest = pictuCanva.CreateGraphics())
            {
                gDest.DrawImageUnscaled(tempBitmap, 0, 0);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos administrados
                    if (pen != null)
                    {
                        pen.Dispose();
                        pen = null;
                    }
                }
                disposed = true;
            }
        }

        ~Linea()
        {
            Dispose(false);
        }
        #endregion
    }
}
