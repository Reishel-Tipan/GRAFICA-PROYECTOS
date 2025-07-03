using Graficar_lineas.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Controllers
{
    public class Cohen_SutherlandController
    {
        private PictureBox pictureBox;
        private Graphics graphics;
        private Bitmap bitmap;
        private Cohen_Sutherland cohenSutherland;
        private Rectangle clipWindow;
        private Point? startPoint;
        private Point? endPoint;
        private bool isClipping = false;

        public Cohen_SutherlandController(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;
        }

        public void SetClipWindow(Rectangle window)
        {
            clipWindow = window;
            cohenSutherland = new Cohen_Sutherland(clipWindow);
            DrawClipWindow();
        }

        public void SetLine(Point start, Point end)
        {
            startPoint = start;
            endPoint = end;
            DrawLine();
        }

        public void ClipLine()
        {
            if (!startPoint.HasValue || !endPoint.HasValue || cohenSutherland == null)
                return;

            Point p1 = startPoint.Value;
            Point p2 = endPoint.Value;

            if (cohenSutherland.ClipLine(ref p1, ref p2))
            {
                // Dibujar la línea recortada en rojo
                using (var pen = new Pen(Color.Red, 2))
                {
                    graphics.DrawLine(pen, p1, p2);
                }
                pictureBox.Invalidate();
            }
        }

        private void DrawClipWindow()
        {
            // Dibujar la ventana de recorte en azul
            using (var pen = new Pen(Color.Blue, 2))
            {
                graphics.DrawRectangle(pen, clipWindow);
            }
            pictureBox.Invalidate();
        }

        private void DrawLine()
        {
            if (!startPoint.HasValue || !endPoint.HasValue)
                return;

            // Dibujar la línea original en verde
            using (var pen = new Pen(Color.Green, 1))
            {
                graphics.DrawLine(pen, startPoint.Value, endPoint.Value);
            }
            pictureBox.Invalidate();
        }

        public void Clear()
        {
            graphics.Clear(Color.White);
            startPoint = null;
            endPoint = null;
            clipWindow = Rectangle.Empty;
            cohenSutherland = null;
            pictureBox.Invalidate();
        }
    }
}
