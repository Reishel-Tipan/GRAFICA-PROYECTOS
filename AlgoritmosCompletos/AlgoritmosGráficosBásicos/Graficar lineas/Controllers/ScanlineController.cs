using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class ScanlineController
    {
        private readonly Bitmap _bitmap;
        private readonly Graphics _graphics;
        private readonly ListBox _listBoxPuntos;
        private readonly Scanline _scanline;
        private readonly Stack<Bitmap> _undoStack = new Stack<Bitmap>();
        private readonly Stack<Bitmap> _redoStack = new Stack<Bitmap>();

        public ScanlineController(Bitmap bitmap, Graphics graphics, ListBox listBoxPuntos)
        {
            _bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            _graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            _listBoxPuntos = listBoxPuntos ?? throw new ArgumentNullException(nameof(listBoxPuntos));
            
            _scanline = new Scanline(_bitmap, _graphics);
        }

        public void SetFillColor(Color color)
        {
            _scanline.SetFillColor(color);
        }

        public void Fill(Point point)
        {
            if (_bitmap == null) return;

            try
            {
                // Guardar estado para deshacer
                _undoStack.Push(new Bitmap(_bitmap));
                _redoStack.Clear();

                // Realizar el relleno
                _scanline.Fill(point);
                
                // Actualizar lista de puntos
                UpdatePointsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el relleno: " + ex.Message, ex);
            }
        }

        private void UpdatePointsList()
        {
            if (_listBoxPuntos.InvokeRequired)
            {
                _listBoxPuntos.Invoke(new Action(UpdatePointsList));
                return;
            }

            _listBoxPuntos.BeginUpdate();
            try
            {
                _listBoxPuntos.Items.Clear();
                if (_scanline.PixelsPintados != null)
                {
                    foreach (var point in _scanline.PixelsPintados)
                    {
                        _listBoxPuntos.Items.Add($"({point.X}, {point.Y})");
                    }
                }
            }
            finally
            {
                _listBoxPuntos.EndUpdate();
            }
        }
    }
}
