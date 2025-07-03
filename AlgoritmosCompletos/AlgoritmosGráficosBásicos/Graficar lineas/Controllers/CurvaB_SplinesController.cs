using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Controllers
{
    public class CurvaB_SplinesController
    {
        private PictureBox pictureBox;
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen pen;
        private Color colorActual = Color.Black;
        private float anchoLinea = 2f;

        public CurvaB_SplinesController(PictureBox pictureBox, Bitmap bitmap)
        {
            this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            this.bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            this.graphics = Graphics.FromImage(this.bitmap);
            this.pen = new Pen(colorActual, anchoLinea);
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        }


        public void AgregarPunto(Point punto)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
            }

            pictureBox.Image = bitmap;
        }

        public void Redibujar(List<PointF> puntos)
        {
            if (puntos.Count < 4) return;

            graphics.Clear(Color.White);

            foreach (var p in puntos)
                graphics.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);

            // Dibujar líneas entre puntos (puedes mejorar con interpolación si deseas)
            graphics.DrawCurve(pen, puntos.ToArray());

            pictureBox.Refresh();
        }

        public async Task AnimarCurvaBSpline(List<PointF> puntos, int numSegmentos = 100, int delayMs = 20)
        {
            if (puntos.Count < 4) return;

            using (Pen penCurva = new Pen(pen.Color, pen.Width))
            using (Pen penLineas = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
            {
                List<PointF> puntosCurva = new List<PointF>();

                for (int i = 0; i <= puntos.Count - 4; i++)
                {
                    PointF prevPoint = CalcularBSpline(puntos[i], puntos[i + 1], puntos[i + 2], puntos[i + 3], 0f);

                    for (float t = 0f; t <= 1.0f; t += 1f / numSegmentos)
                    {
                        PointF currentPoint = CalcularBSpline(puntos[i], puntos[i + 1], puntos[i + 2], puntos[i + 3], t);

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.Clear(Color.White);

                            // Dibujar puntos de control en rojo
                            foreach (var p in puntos)
                            {
                                g.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);
                            }

                            // Dibujar líneas auxiliares (opcional)
                            for (int j = 0; j < puntos.Count - 1; j++)
                            {
                                g.DrawLine(penLineas, puntos[j], puntos[j + 1]);
                            }

                            // Dibujar curva acumulada
                            puntosCurva.Add(currentPoint);
                            if (puntosCurva.Count > 1)
                            {
                                g.DrawLines(penCurva, puntosCurva.ToArray());
                            }
                        }

                        pictureBox.Image = bitmap;

                        prevPoint = currentPoint;
                        await Task.Delay(delayMs);
                    }
                }
            }
        }



        private PointF CalcularBSpline(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            float x =
                ((-p0.X + 3 * p1.X - 3 * p2.X + p3.X) * (float)Math.Pow(t, 3) +
                 (3 * p0.X - 6 * p1.X + 3 * p2.X) * (float)Math.Pow(t, 2) +
                 (-3 * p0.X + 3 * p2.X) * t +
                 (p0.X + 4 * p1.X + p2.X)) / 6;

            float y =
                ((-p0.Y + 3 * p1.Y - 3 * p2.Y + p3.Y) * (float)Math.Pow(t, 3) +
                 (3 * p0.Y - 6 * p1.Y + 3 * p2.Y) * (float)Math.Pow(t, 2) +
                 (-3 * p0.Y + 3 * p2.Y) * t +
                 (p0.Y + 4 * p1.Y + p2.Y)) / 6;

            return new PointF(x, y);
        }

        public void Limpiar()
        {
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;
        }

        public void ActualizarColor(Color nuevoColor)
        {
            colorActual = nuevoColor;
            pen.Color = colorActual;
        }
    }
}
