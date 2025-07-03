using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class DDAController
    {
        private DDA _dda;
        private PictureBox _pictureBox;
        private ListBox _listBoxPuntos;
        private TextBox _txtX1, _txtY1, _txtX2, _txtY2;
        private Button _btnGraficar, _btnReseat;

        public DDAController(
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
                // Crear una nueva instancia de DDA
                _dda = new DDA(_pictureBox);
                
                // Suscribirse al evento de actualización de puntos
                _dda.PuntosActualizados += (s, args) =>
                {
                    if (_listBoxPuntos.InvokeRequired)
                    {
                        _listBoxPuntos.Invoke(new Action(ActualizarListaPuntos));
                    }
                    else
                    {
                        ActualizarListaPuntos();
                    }
                };

                // Leer los datos y graficar
                if (_dda.ReadData(_txtX1, _txtX2, _txtY1, _txtY2))
                {
                    _dda.PlotShape(_pictureBox);
                    // Habilitar el botón de reinicio
                    _btnReseat.Enabled = true;
                }
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
            if (_dda == null || _dda.PuntosDibujados == null) return;
            
            _listBoxPuntos.BeginUpdate();
            try
            {
                _listBoxPuntos.Items.Clear();
                
                // Obtener los puntos ordenados
                var puntosOrdenados = _dda.ObtenerPuntosOrdenados();
                
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
