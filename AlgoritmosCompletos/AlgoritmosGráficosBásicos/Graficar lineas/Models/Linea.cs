using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class Linea : IDisposable
    {
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

        public async Task AnimarCurvaBezier(Graphics canvas, List<PointF> puntos, int numSegmentos, PictureBox pictuCanva, Bitmap bm, int delayMs = 20)
        {
            if (puntos == null || puntos.Count < 2 || pictuCanva == null || bm == null) 
                return;

            try
            {
                using (Pen penCurva = new Pen(pen.Color, pen.Width))
                using (Pen penLineas = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
                {
                    // Limpiar y dibujar puntos de control
                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        g.Clear(Color.White);
                        foreach (var punto in puntos)
                        {
                            g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                        }
                        pictuCanva.Refresh();
                    }

                    List<PointF> puntosCurva = new List<PointF>();
                    
                    for (float t = 0; t <= 1; t += 0.01f)
                    {
                        PointF punto = CalcularPuntoBezier(puntos, t);
                        puntosCurva.Add(punto);
                        
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.Clear(Color.White);
                            
                            // Dibujar puntos de control
                            foreach (var p in puntos)
                            {
                                g.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);
                            }
                            
                            // Dibujar líneas de construcción
                            DibujarPasoAnimacion(g, puntos, t, penLineas, penCurva, ref puntosCurva);
                            
                            pictuCanva.Refresh();
                        }
                        
                        await Task.Delay(delayMs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private PointF CalcularPuntoBezier(List<PointF> puntos, float t)
        {
            // Implementación simple del algoritmo de De Casteljau
            if (puntos.Count == 1)
                return puntos[0];
                
            List<PointF> nuevosPuntos = new List<PointF>();
            
            for (int i = 0; i < puntos.Count - 1; i++)
            {
                float x = (1 - t) * puntos[i].X + t * puntos[i + 1].X;
                float y = (1 - t) * puntos[i].Y + t * puntos[i + 1].Y;
                nuevosPuntos.Add(new PointF(x, y));
            }
            
            return CalcularPuntoBezier(nuevosPuntos, t);
        }
        
        private void DibujarPasoAnimacion(Graphics g, List<PointF> puntos, float t, Pen penLineas, Pen penCurva, ref List<PointF> puntosCurva)
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
                        g.DrawLine(penLineas, puntosActuales[i], puntosActuales[i + 1]);
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

        // Implementación de IDisposable
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
    }
}
