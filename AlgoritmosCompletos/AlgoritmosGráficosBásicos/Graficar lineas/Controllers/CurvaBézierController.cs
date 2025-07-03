using Graficar_lineas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Controllers
{
    public class CurvaBézierController : IDisposable
    {
        private bool disposed = false;
        private PictureBox pictureBox;
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen pen;
        private Linea linea;
        private bool isDrawing = false;
        private List<PointF> puntosControl = new List<PointF>();
        private Color colorActual = Color.Black;
        private float anchoLinea = 2f;

        public List<PointF> PuntosControl => new List<PointF>(puntosControl);

        public CurvaBézierController(PictureBox pictureBox, Bitmap bitmap)
        {
            this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            this.bitmap = new Bitmap(bitmap ?? throw new ArgumentNullException(nameof(bitmap)));
            this.graphics = Graphics.FromImage(this.bitmap);
            this.pen = new Pen(colorActual, anchoLinea);
            this.linea = new Linea(graphics, pen);
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void AgregarPunto(Point punto)
        {
            if (isDrawing) return;

            puntosControl.Add(punto);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
            }
            pictureBox.Image = bitmap;
        }

        public void DibujarCurva()
        {
            if (puntosControl.Count < 2) return;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                
                // Dibujar puntos de control
                foreach (var punto in puntosControl)
                {
                    g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                }
                
                // Dibujar líneas de control
                if (puntosControl.Count > 1)
                {
                    g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                }
                
                // Dibujar la curva de Bézier
                linea.DrawBezierCurve(puntosControl);
            }

            pictureBox.Refresh();
        }

        public async Task AnimarCurva(int numSegmentos = 100, int delayMs = 20)
        {
            if (puntosControl.Count < 2) return;
            
            isDrawing = true;
            
            try
            {
                await linea.AnimarCurvaBezier(
                    graphics,
                    puntosControl,
                    numSegmentos,
                    pictureBox,
                    bitmap,
                    delayMs
                );
            }
            finally
            {
                isDrawing = false;
            }
        }

        public void Limpiar()
        {
            try
            {
                puntosControl.Clear();
                
                // Crear un nuevo bitmap limpio
                var nuevoBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                using (var g = Graphics.FromImage(nuevoBitmap))
                {
                    g.Clear(Color.White);
                }

                // Liberar recursos del bitmap anterior
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }

                // Asignar el nuevo bitmap
                bitmap = nuevoBitmap;
                pictureBox.Image = bitmap;

                // Recrear el objeto Graphics
                if (graphics != null)
                {
                    graphics.Dispose();
                }
                graphics = Graphics.FromImage(bitmap);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Recrear la línea con el nuevo objeto Graphics
                var oldLinea = linea;
                linea = new Linea(graphics, new Pen(colorActual, anchoLinea));
                oldLinea?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar: {ex.Message}");
                throw;
            }
        }

        public void ActualizarColor(Color color)
        {
            if (color == null) return;
            
            colorActual = color;
            
            // Crear un nuevo lápiz con el color actualizado
            var oldPen = pen;
            pen = new Pen(color, anchoLinea);
            
            // Actualizar el lápiz en el objeto linea
            if (linea != null)
            {
                linea.ActualizarLapiz(pen);
            }
            
            // Liberar el lápiz anterior
            oldPen?.Dispose();
        }

        public void ActualizarGrosor(int grosor)
        {
            if (grosor <= 0) return;
            
            anchoLinea = grosor;
            pen.Width = anchoLinea;
        }

        public void Redibujar()
        {
            if (puntosControl.Count < 2) return;
            
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                
                // Dibujar puntos de control
                foreach (var punto in puntosControl)
                {
                    g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                }
                
                // Dibujar líneas de control
                if (puntosControl.Count > 1)
                {
                    g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                }
                
                // Dibujar la curva de Bézier
                linea.DrawBezierCurve(puntosControl);
            }
            
            pictureBox.Refresh();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (pen != null) pen.Dispose();
                    if (graphics != null) graphics.Dispose();
                    if (bitmap != null) bitmap.Dispose();
                    if (linea != null) linea.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CurvaBézierController()
        {
            Dispose(false);
        }
    }
}
