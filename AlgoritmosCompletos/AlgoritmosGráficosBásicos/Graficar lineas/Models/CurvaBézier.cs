using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class CurvaBézier
    {
        private Graphics graphics;
        private Pen pen;
        private List<PointF> puntosControl;
        private Bitmap bitmap;
        private PictureBox pictureBox;

        public List<PointF> PuntosControl => puntosControl;

        public CurvaBézier(PictureBox pictureBox, Bitmap bitmap, Color color, float anchoLinea = 2f)
        {
            this.pictureBox = pictureBox;
            this.bitmap = bitmap;
            this.graphics = Graphics.FromImage(bitmap);
            this.pen = new Pen(color, anchoLinea);
            this.puntosControl = new List<PointF>();
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void AgregarPuntoControl(PointF punto)
        {
            puntosControl.Add(punto);
            DibujarPuntosControl();
        }

        public void LimpiarPuntos()
        {
            puntosControl.Clear();
            Redibujar();
        }

        public void DibujarCurva()
        {
            if (puntosControl.Count < 2) return;

            // Dibujar la curva de Bézier
            if (puntosControl.Count == 2)            {                graphics.DrawLine(pen, puntosControl[0], puntosControl[1]);            }            else if (puntosControl.Count == 3)            {                graphics.DrawBezier(pen, puntosControl[0], puntosControl[1], puntosControl[1], puntosControl[2]);            }            else if (puntosControl.Count >= 4)            {                // Para más de 3 puntos, dibujar una curva compuesta
                var puntos = puntosControl.ToArray();
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())                {                    path.AddBeziers(puntos);                    graphics.DrawPath(pen, path);                }            }

            pictureBox.Image = bitmap;
        }

        public void DibujarPuntosControl()
        {
            foreach (var punto in puntosControl)
            {
                graphics.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
            }
            pictureBox.Image = bitmap;
        }

        public void Redibujar()
        {
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;
        }

        public void ActualizarColor(Color nuevoColor)
        {
            pen.Color = nuevoColor;
        }

        public void ActualizarAnchoLinea(float ancho)
        {
            pen.Width = ancho;
        }
    }
}
