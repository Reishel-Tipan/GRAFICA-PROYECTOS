using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class Scanline
    {
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Color _fillColor;
        private Color _borderColor;
        private List<Point> _pixelsPintados;

        public List<Point> PixelsPintados => _pixelsPintados;

        public Scanline(Bitmap bitmap, Graphics graphics)
        {
            _bitmap = bitmap;
            _graphics = graphics;
            _pixelsPintados = new List<Point>();
            _fillColor = Color.Black; // Color por defecto
            _borderColor = Color.Black; // Color del borde por defecto
        }

        public void SetFillColor(Color color)
        {
            _fillColor = color;
        }

        public void SetBorderColor(Color color)
        {
            _borderColor = color;
        }

        public void Fill(Point seedPoint)
        {
            if (_bitmap == null) return;

            Color targetColor = _bitmap.GetPixel(seedPoint.X, seedPoint.Y);
            if (targetColor.ToArgb() == _fillColor.ToArgb()) return;

            _pixelsPintados.Clear();
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(seedPoint);

            while (pixels.Count > 0)
            {
                Point current = pixels.Pop();
                int y1 = current.Y;
                
                // Encontrar el límite superior
                while (y1 >= 0 && _bitmap.GetPixel(current.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;

                bool spanLeft = false;
                bool spanRight = false;

                // Rellenar la línea de escaneo
                while (y1 < _bitmap.Height && _bitmap.GetPixel(current.X, y1) == targetColor)
                {
                    _bitmap.SetPixel(current.X, y1, _fillColor);
                    _pixelsPintados.Add(new Point(current.X, y1));

                    // Verificar píxeles a la izquierda
                    if (!spanLeft && current.X > 0 && _bitmap.GetPixel(current.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(current.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && current.X > 0 && _bitmap.GetPixel(current.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }

                    // Verificar píxeles a la derecha
                    if (!spanRight && current.X < _bitmap.Width - 1 && _bitmap.GetPixel(current.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(current.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && current.X < _bitmap.Width - 1 && _bitmap.GetPixel(current.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }

                    y1++;
                }
            }
        }
    }
}
