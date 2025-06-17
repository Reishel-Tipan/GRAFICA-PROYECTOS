using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public partial class FrmRellenado : Form
    {
        private RellenoRombo rombo;
        private bool dibujando = false;

        public FrmRellenado()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Relleno de Rombo";
        }

        private void FrmRellenado_Load(object sender, EventArgs e)
        {
            // Inicializar el PictureBox con un borde
            picDibujo.BorderStyle = BorderStyle.FixedSingle;
            picDibujo.BackColor = Color.White;
            
            // Inicializar el objeto rombo
            rombo = new RellenoRombo(picDibujo);
            
            // Configurar eventos
            btnGraficar.Click += BtnGraficar_Click;
            btnReseat.Click += BtnReseat_Click;
            picDibujo.MouseClick += PicDibujo_MouseClick;
        }

        private void BtnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar entradas
                if (!int.TryParse(txtX_1.Text, out int x) ||
                    !int.TryParse(txtY_1.Text, out int y) ||
                    !int.TryParse(txtDiagonal.Text, out int diagonal))
                {
                    MessageBox.Show("Por favor ingrese valores numéricos válidos.", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Configurar y dibujar el rombo
                rombo.ConfigurarRombo(x, y, diagonal);
                rombo.Dibujar();
                
                // Actualizar la lista de vértices
                ActualizarListaVertices();
                
                // Forzar el redibujado
                picDibujo.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar el rombo: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReseat_Click(object sender, EventArgs e)
        {
            // Limpiar todo
            rombo.Limpiar();
            listBoxVértices.Items.Clear();
            dibujando = false;
            
            // Limpiar campos de entrada
            txtX_1.Clear();
            txtY_1.Clear();
            txtDiagonal.Clear();
        }

        private void PicDibujo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && rombo.Vertices.Count == 4)
            {
                rombo.Rellenar(e.Location);
            }
        }

        private void ActualizarListaVertices()
        {
            listBoxVértices.Items.Clear();
            
            if (rombo.Vertices.Count > 0)
            {
                // Obtener el centro del PictureBox
                int centroX = picDibujo.Width / 2;
                int centroY = picDibujo.Height / 2;
                
                string[] puntos = new string[4];
                
                // Convertir coordenadas de píxeles a coordenadas cartesianas
                int x1 = (rombo.Vertices[0].X - centroX) / rombo.FactorEscala;
                int y1 = (centroY - rombo.Vertices[0].Y) / rombo.FactorEscala;
                int x2 = (rombo.Vertices[1].X - centroX) / rombo.FactorEscala;
                int y2 = (centroY - rombo.Vertices[1].Y) / rombo.FactorEscala;
                int x3 = (rombo.Vertices[2].X - centroX) / rombo.FactorEscala;
                int y3 = (centroY - rombo.Vertices[2].Y) / rombo.FactorEscala;
                int x4 = (rombo.Vertices[3].X - centroX) / rombo.FactorEscala;
                int y4 = (centroY - rombo.Vertices[3].Y) / rombo.FactorEscala;
                
                puntos[0] = $"Vértice 1 (Arriba): ({x1}, {y1})";
                puntos[1] = $"Vértice 2 (Derecha): ({x2}, {y2})";
                puntos[2] = $"Vértice 3 (Abajo): ({x3}, {y3})";
                puntos[3] = $"Vértice 4 (Izquierda): ({x4}, {y4})";
                
                listBoxVértices.Items.AddRange(puntos);
            }
        }
    }
}
