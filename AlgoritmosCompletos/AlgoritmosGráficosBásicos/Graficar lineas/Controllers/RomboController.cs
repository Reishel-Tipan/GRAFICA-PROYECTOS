using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class RomboController
    {
        private readonly RellenoRombo _rombo;
        private readonly PictureBox _pictureBox;
        private readonly ListBox _listBoxVertices;
        private readonly TextBox _txtX, _txtY, _txtDiagonal;
        private readonly Button _btnGraficar, _btnReseat;

        public RomboController(
            PictureBox pictureBox,
            ListBox listBoxVertices,
            TextBox txtX, TextBox txtY, TextBox txtDiagonal,
            Button btnGraficar, Button btnReseat)
        {
            _pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            _listBoxVertices = listBoxVertices ?? throw new ArgumentNullException(nameof(listBoxVertices));
            _txtX = txtX ?? throw new ArgumentNullException(nameof(txtX));
            _txtY = txtY ?? throw new ArgumentNullException(nameof(txtY));
            _txtDiagonal = txtDiagonal ?? throw new ArgumentNullException(nameof(txtDiagonal));
            _btnGraficar = btnGraficar ?? throw new ArgumentNullException(nameof(btnGraficar));
            _btnReseat = btnReseat ?? throw new ArgumentNullException(nameof(btnReseat));

            // Inicializar el rombo
            _rombo = new RellenoRombo(_pictureBox);

            // Configurar manejadores de eventos
            _btnGraficar.Click += BtnGraficar_Click;
            _btnReseat.Click += BtnReseat_Click;
            _pictureBox.MouseClick += PicDibujo_MouseClick;
            _btnReseat.Enabled = false;
        }

        public void BtnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar entradas
                if (!int.TryParse(_txtX.Text, out int x) ||
                    !int.TryParse(_txtY.Text, out int y) ||
                    !int.TryParse(_txtDiagonal.Text, out int diagonal) || diagonal <= 0)
                {
                    MessageBox.Show("Por favor ingrese valores numéricos válidos. La diagonal debe ser positiva.", 
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Configurar y dibujar el rombo
                _rombo.ConfigurarRombo(x, y, diagonal);
                _rombo.Dibujar();
                
                // Actualizar la lista de vértices
                ActualizarListaVertices();
                
                // Habilitar el botón de limpiar
                _btnReseat.Enabled = true;
                
                // Forzar el redibujado
                _pictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar el rombo: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BtnReseat_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar todo
                _rombo.Limpiar();
                _listBoxVertices.Items.Clear();
                
                // Limpiar campos de entrada
                _txtX.Clear();
                _txtY.Clear();
                _txtDiagonal.Clear();
                
                // Deshabilitar el botón de limpiar
                _btnReseat.Enabled = false;
                
                // Establecer el foco en el primer campo
                _txtX.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PicDibujo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _rombo.Vertices.Count == 4)
            {
                _rombo.Rellenar(e.Location);
            }
        }

        private void ActualizarListaVertices()
        {
            _listBoxVertices.Items.Clear();
            
            if (_rombo.Vertices.Count > 0)
            {
                // Obtener el centro del PictureBox
                int centroX = _pictureBox.Width / 2;
                int centroY = _pictureBox.Height / 2;
                
                string[] puntos = new string[4];
                
                // Convertir coordenadas de píxeles a coordenadas cartesianas
                int x1 = (_rombo.Vertices[0].X - centroX) / _rombo.FactorEscala;
                int y1 = (centroY - _rombo.Vertices[0].Y) / _rombo.FactorEscala;
                int x2 = (_rombo.Vertices[1].X - centroX) / _rombo.FactorEscala;
                int y2 = (centroY - _rombo.Vertices[1].Y) / _rombo.FactorEscala;
                int x3 = (_rombo.Vertices[2].X - centroX) / _rombo.FactorEscala;
                int y3 = (centroY - _rombo.Vertices[2].Y) / _rombo.FactorEscala;
                int x4 = (_rombo.Vertices[3].X - centroX) / _rombo.FactorEscala;
                int y4 = (centroY - _rombo.Vertices[3].Y) / _rombo.FactorEscala;
                
                puntos[0] = $"Vértice 1 (Arriba): ({x1}, {y1})";
                puntos[1] = $"Vértice 2 (Derecha): ({x2}, {y2})";
                puntos[2] = $"Vértice 3 (Abajo): ({x3}, {y3})";
                puntos[3] = $"Vértice 4 (Izquierda): ({x4}, {y4})";
                
                _listBoxVertices.Items.AddRange(puntos);
            }
        }
    }
}
