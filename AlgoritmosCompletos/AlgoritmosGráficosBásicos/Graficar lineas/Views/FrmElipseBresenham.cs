using System;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas.Views
{
    public partial class FrmElipseBresenham : Form
    {
        private ElipseBreshController _controller;

        public FrmElipseBresenham()
        {
            InitializeComponent();
            InicializarControlador();
        }


        private void InicializarControlador()
        {
            if (picCanvas.Width <= 0 || picCanvas.Height <= 0)
            {
                MessageBox.Show("Error: El área de dibujo no tiene un tamaño válido.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _controller = new ElipseBreshController(
                    picCanvas,           // PictureBox para dibujar
                    listBoxPuntos,       // ListBox para mostrar puntos
                    txtCentroX,          // TextBox para coordenada X del centro
                    txtCentroY,          // TextBox para coordenada Y del centro
                    txtRadioMayor,       // TextBox para radio mayor
                    txtRadioMenor,       // TextBox para radio menor
                    btnGraficar,         // Botón Graficar
                    btnLimpiar           // Botón Limpiar
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el controlador: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmElipseBresenham_Load(object sender, EventArgs e)
        {
            // Configurar el manejador de eventos para el botón Limpiar
            this.btnLimpiar.Click += BtnLimpiar_Click;
        }

        private void picCanvas_Click(object sender, EventArgs e)
        {
            // Implementación del evento de clic en el canvas si es necesario
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar el PictureBox
                if (picCanvas.Image != null)
                {
                    using (Graphics g = Graphics.FromImage(picCanvas.Image))
                    {
                        g.Clear(picCanvas.BackColor);
                    }
                    picCanvas.Invalidate();
                }
                
                // Limpiar el ListBox
                listBoxPuntos.Items.Clear();
                
                // Limpiar los campos de texto
                txtCentroX.Clear();
                txtCentroY.Clear();
                txtRadioMayor.Clear();
                txtRadioMenor.Clear();
                
                // Establecer el foco en el primer campo
                txtCentroX.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
