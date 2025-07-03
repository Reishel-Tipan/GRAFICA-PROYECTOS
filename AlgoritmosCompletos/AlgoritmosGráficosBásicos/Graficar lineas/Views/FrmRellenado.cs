using System;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas
{
    public partial class FrmRellenado : Form
    {
        private RomboController _controller;

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
            
            // Inicializar el controlador
            _controller = new RomboController(
                picDibujo,
                listBoxVértices,
                txtX_1, txtY_1, txtDiagonal,
                btnGraficar, btnReseat);
        }

        private void BtnGraficar_Click(object sender, EventArgs e)
        {
            _controller.BtnGraficar_Click(sender, e);
        }

        private void BtnReseat_Click(object sender, EventArgs e)
        {
            _controller.BtnReseat_Click(sender, e);
        }

        private void PicDibujo_MouseClick(object sender, MouseEventArgs e)
        {
            _controller.PicDibujo_MouseClick(sender, e);
        }

        private void btnReseat_Click_1(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
