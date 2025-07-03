using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class CircunferenciaController
    {
        private Circunferencia _circunferencia;
        private readonly PictureBox _pictureBox;
        private readonly ListBox _listBoxPuntos;
        private readonly TextBox _txtX, _txtY, _txtRadio;
        private readonly Button _btnGraficar, _btnReseat;
        private Bitmap _bmp;
        private Graphics _gBmp;
        private int _cellSize = 10;
        private int _centerX, _centerY;
        private int _canvasSize;
        private int _marginX, _marginY;

        public CircunferenciaController(
            PictureBox pictureBox,
            ListBox listBoxPuntos,
            TextBox txtX, TextBox txtY, TextBox txtRadio,
            Button btnGraficar, Button btnReseat)
        {
            _pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            _listBoxPuntos = listBoxPuntos ?? throw new ArgumentNullException(nameof(listBoxPuntos));
            _txtX = txtX ?? throw new ArgumentNullException(nameof(txtX));
            _txtY = txtY ?? throw new ArgumentNullException(nameof(txtY));
            _txtRadio = txtRadio ?? throw new ArgumentNullException(nameof(txtRadio));
            _btnGraficar = btnGraficar ?? throw new ArgumentNullException(nameof(btnGraficar));
            _btnReseat = btnReseat ?? throw new ArgumentNullException(nameof(btnReseat));

            // Configurar manejadores de eventos
            _btnGraficar.Click += BtnGraficar_Click;
            _btnReseat.Click += BtnReseat_Click;
            _btnReseat.Enabled = false;

            // Inicializar el área de dibujo
            InicializarAreaDibujo();
        }

        private void InicializarAreaDibujo()
        {
            // Crear un nuevo bitmap para dibujar
            _bmp = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            _gBmp = Graphics.FromImage(_bmp);
            
            // Calcular dimensiones
            _canvasSize = Math.Min(_pictureBox.Width, _pictureBox.Height) / _cellSize;
            _marginX = (_pictureBox.Width - _canvasSize * _cellSize) / 2;
            _marginY = (_pictureBox.Height - _canvasSize * _cellSize) / 2;
            
            // Calcular el centro del área de dibujo
            _centerX = _canvasSize / 2;
            _centerY = _canvasSize / 2;
            
            LimpiarAreaDibujo();
        }

        private void LimpiarAreaDibujo()
        {
            // Limpiar el área de dibujo
            _gBmp.Clear(Color.White);
            
            // Dibujar cuadrícula
            using (var gridPen = new Pen(Color.LightGray, 1))
            {
                for (int i = 0; i <= _canvasSize; i++)
                {
                    int x = _marginX + i * _cellSize;
                    _gBmp.DrawLine(gridPen, x, _marginY, x, _marginY + _canvasSize * _cellSize);
                    
                    int y = _marginY + i * _cellSize;
                    _gBmp.DrawLine(gridPen, _marginX, y, _marginX + _canvasSize * _cellSize, y);
                }
            }
            
            // Dibujar ejes
            using (var axisPen = new Pen(Color.Black, 2))
            {
                _gBmp.DrawLine(axisPen, _marginX, _marginY + _centerY * _cellSize, 
                    _marginX + _canvasSize * _cellSize, _marginY + _centerY * _cellSize); // Eje X
                _gBmp.DrawLine(axisPen, _marginX + _centerX * _cellSize, _marginY, 
                    _marginX + _centerX * _cellSize, _marginY + _canvasSize * _cellSize); // Eje Y
            }
            
            // Actualizar el PictureBox
            _pictureBox.Image = _bmp;
        }

        public void BtnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos tengan valores
                if (string.IsNullOrWhiteSpace(_txtX.Text) || string.IsNullOrWhiteSpace(_txtY.Text) || 
                    string.IsNullOrWhiteSpace(_txtRadio.Text))
                {
                    MessageBox.Show("Por favor ingrese todos los valores.", "Datos incompletos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convertir y validar que sean números enteros
                if (!int.TryParse(_txtX.Text, out int x) || 
                    !int.TryParse(_txtY.Text, out int y) || 
                    !int.TryParse(_txtRadio.Text, out int radio) ||
                    radio <= 0)
                {
                    MessageBox.Show("Por favor ingrese valores numéricos válidos. El radio debe ser positivo.", 
                                  "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Limpiar el área de dibujo y la lista de puntos
                LimpiarAreaDibujo();
                _listBoxPuntos.Items.Clear();
                
                // Crear una nueva instancia de Circunferencia
                _circunferencia = new Circunferencia(_pictureBox);
                
                // Validar y configurar los datos de la circunferencia
                if (_circunferencia.ValidarDatos(x.ToString(), y.ToString(), radio.ToString(), _pictureBox))
                {
                    // Iniciar la animación
                    _circunferencia.IniciarAnimacion(ActualizarDibujo);
                }
                else
                {
                    _circunferencia = null;
                    return;
                }
                
                // Habilitar el botón de reinicio
                _btnReseat.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al graficar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarDibujo()
        {
            if (_circunferencia == null) return;
            
            // Limpiar el área de dibujo
            LimpiarAreaDibujo();
            
            // Obtener los puntos actuales
            var puntos = _circunferencia.ObtenerPuntos();
            if (puntos == null || puntos.Count == 0) return;
            
            // Dibujar cada punto
            using (var brush = new SolidBrush(Color.Blue))
            {
                foreach (var punto in puntos)
                {
                    // Calcular las coordenadas del píxel en la cuadrícula
                    // El centro de la cuadrícula está en (_centerX, _centerY)
                    // Convertir coordenadas del mundo a coordenadas de pantalla
                    int screenX = _marginX + (_centerX + punto.X) * _cellSize;
                    int screenY = _marginY + (_centerY - punto.Y) * _cellSize;
                    
                    // Asegurarse de que las coordenadas estén dentro de los límites
                    if (screenX >= _marginX && screenX < _marginX + _canvasSize * _cellSize && 
                        screenY >= _marginY && screenY < _marginY + _canvasSize * _cellSize)
                    {
                        _gBmp.FillRectangle(brush, screenX, screenY, _cellSize, _cellSize);
                    }
                }
            }
            
            
            // Actualizar el PictureBox
            _pictureBox.Refresh();
            
            // Actualizar la lista de puntos en el ListBox
            MostrarTodosLosPuntos();
        }

        private void MostrarTodosLosPuntos()
        {
            if (_circunferencia == null) return;
            
            // Obtener los puntos actuales
            var puntos = _circunferencia.ObtenerPuntos();
            
            // Verificar si hay puntos para mostrar
            if (puntos.Count == 0) return;
            
            // Usar BeginUpdate/EndUpdate para mejorar el rendimiento
            _listBoxPuntos.BeginUpdate();
            try
            {
                // Limpiar solo si es necesario (para evitar parpadeo)
                if (_listBoxPuntos.Items.Count != puntos.Count)
                {
                    _listBoxPuntos.Items.Clear();
                    
                    // Ordenar los puntos para mostrarlos de manera más organizada
                    var puntosOrdenados = new List<Point>(puntos);
                    puntosOrdenados.Sort((p1, p2) => 
                        p1.X != p2.X ? p1.X.CompareTo(p2.X) : p1.Y.CompareTo(p2.Y));
                    
                    // Agregar los puntos al ListBox
                    foreach (var punto in puntosOrdenados)
                    {
                        _listBoxPuntos.Items.Add($"({punto.X}, {punto.Y})");
                    }
                }
            }
            finally
            {
                _listBoxPuntos.EndUpdate();
            }
        }

        public void BtnReseat_Click(object sender, EventArgs e)
        {
            try
            {
                // Detener la animación si está en curso
                if (_circunferencia?.AnimacionEnCurso == true)
                {
                    _circunferencia.DetenerAnimacion();
                }
                
                // Limpiar los controles
                _txtX.Clear();
                _txtY.Clear();
                _txtRadio.Clear();
                
                // Limpiar el área de dibujo
                LimpiarAreaDibujo();
                
                // Limpiar la lista de puntos
                _listBoxPuntos.Items.Clear();
                
                // Deshabilitar el botón de reinicio
                _btnReseat.Enabled = false;
                
                // Establecer el foco en el primer campo
                _txtX.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
