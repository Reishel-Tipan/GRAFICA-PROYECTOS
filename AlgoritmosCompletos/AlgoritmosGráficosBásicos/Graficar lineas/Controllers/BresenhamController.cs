using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class BresenhamController
    {
        private Bresenham _bresenham;
        private readonly PictureBox _pictureBox;
        private readonly ListBox _listBoxPuntos;
        private readonly TextBox _txtX1, _txtY1, _txtX2, _txtY2;
        private readonly Button _btnGraficar, _btnReseat;

        public BresenhamController(
            PictureBox pictureBox,
            ListBox listBoxPuntos,
            TextBox txtX1, TextBox txtY1,
            TextBox txtX2, TextBox txtY2,
            Button btnGraficar, Button btnReseat)
        {
            _pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            _listBoxPuntos = listBoxPuntos ?? throw new ArgumentNullException(nameof(listBoxPuntos));
            _txtX1 = txtX1 ?? throw new ArgumentNullException(nameof(txtX1));
            _txtY1 = txtY1 ?? throw new ArgumentNullException(nameof(txtY1));
            _txtX2 = txtX2 ?? throw new ArgumentNullException(nameof(txtX2));
            _txtY2 = txtY2 ?? throw new ArgumentNullException(nameof(txtY2));
            _btnGraficar = btnGraficar ?? throw new ArgumentNullException(nameof(btnGraficar));
            _btnReseat = btnReseat ?? throw new ArgumentNullException(nameof(btnReseat));

            // Configurar manejadores de eventos
            _btnGraficar.Click += BtnGraficar_Click;
            _btnReseat.Click += BtnReseat_Click;
            _btnReseat.Enabled = false;
        }

        public void BtnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos tengan valores
                if (string.IsNullOrWhiteSpace(_txtX1.Text) || string.IsNullOrWhiteSpace(_txtX2.Text) || 
                    string.IsNullOrWhiteSpace(_txtY1.Text) || string.IsNullOrWhiteSpace(_txtY2.Text))
                {
                    MessageBox.Show("Por favor ingrese todos los valores.", "Datos incompletos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convertir y validar que sean números positivos
                if (!int.TryParse(_txtX1.Text, out int x1) || x1 < 0 ||
                    !int.TryParse(_txtX2.Text, out int x2) || x2 < 0 ||
                    !int.TryParse(_txtY1.Text, out int y1) || y1 < 0 ||
                    !int.TryParse(_txtY2.Text, out int y2) || y2 < 0)
                {
                    MessageBox.Show("Por favor ingrese solo números enteros positivos.", "Datos inválidos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Limpiar el ListBox
                _listBoxPuntos.Items.Clear();
                
                // Crear una instancia de la clase Bresenham, pasando el PictureBox
                _bresenham = new Bresenham(_pictureBox);
                
                // Suscribirse al evento de actualización de puntos
                _bresenham.PuntosActualizados += (s, args) => 
                {
                    // Actualizar la lista de puntos en el hilo de la interfaz de usuario
                    if (_listBoxPuntos.InvokeRequired)
                    {
                        _listBoxPuntos.Invoke(new Action(ActualizarListaPuntos));
                    }
                    else
                    {
                        ActualizarListaPuntos();
                    }
                };
                
                // Establecer los puntos y comenzar la animación
                _bresenham.SetPoints(x1, y1, x2, y2);
                
                // Habilitar el botón de reinicio
                _btnReseat.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al graficar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BtnReseat_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar los campos de texto
                _txtX1.Clear();
                _txtY1.Clear();
                _txtX2.Clear();
                _txtY2.Clear();
                
                // Limpiar el ListBox
                _listBoxPuntos.Items.Clear();
                
                // Limpiar el PictureBox
                if (_pictureBox.Image != null)
                {
                    using (var g = Graphics.FromImage(_pictureBox.Image))
                    {
                        g.Clear(_pictureBox.BackColor);
                    }
                    _pictureBox.Invalidate();
                }
                
                // Detener cualquier animación en curso
                if (_bresenham != null)
                {
                    var temp = _bresenham;
                    _bresenham = null;
                    
                    // Forzar recolección de basura para liberar recursos
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                
                // Deshabilitar el botón de reinicio
                _btnReseat.Enabled = false;
                
                // Establecer el foco en el primer campo
                _txtX1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ActualizarListaPuntos()
        {
            if (_bresenham == null || _bresenham.PuntosDibujados == null) return;
            
            _listBoxPuntos.BeginUpdate();
            try
            {
                _listBoxPuntos.Items.Clear();
                
                // Obtener los puntos ordenados
                var puntosOrdenados = _bresenham.ObtenerPuntosOrdenados();
                
                // Agregar los puntos al ListBox
                foreach (var punto in puntosOrdenados)
                {
                    _listBoxPuntos.Items.Add($"({punto.X}, {punto.Y})");
                }
            }
            finally
            {
                _listBoxPuntos.EndUpdate();
            }
        }
    }
}
