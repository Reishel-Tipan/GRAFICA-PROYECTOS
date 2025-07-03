using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas
{
    public partial class FrmDDA : Form
    { 
        private DDAController _controller;

        public FrmDDA()
        {
            InitializeComponent();
            
            // Configurar el formulario como ventana de herramientas con borde fijo
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Inicializar el controlador
            _controller = new DDAController(
                picDibujo,
                listBoxPuntos,
                txtX_1, txtY_1,
                txtX_2, txtY_2,
                btnGraficar, btnReseat);
            
            // Configurar validación de entrada
            ConfigurarValidacionEntrada();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void labelx1_Click(object sender, EventArgs e)
        {

        }

        private void BtnGraficar_Click(object sender, EventArgs e)
        {
            _controller.BtnGraficar_Click(sender, e);
        }

        private void BtnReseat_Click(object sender, EventArgs e)
        {
            _controller.BtnReseat_Click(sender, e);
        }

        private void txtX_2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtY_2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtY_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtX_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void picDibujo_Click(object sender, EventArgs e)
        {

        }

        private void labelY1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        // El manejador de eventos ahora está en el controlador
        
        // La lógica de actualización de puntos ahora está en el controlador
        
        private void ConfigurarValidacionEntrada()
        {
            // Solo permitir números en los campos de texto
            txtX_1.KeyPress += SoloNumeros_KeyPress;
            txtY_1.KeyPress += SoloNumeros_KeyPress;
            txtX_2.KeyPress += SoloNumeros_KeyPress;
            txtY_2.KeyPress += SoloNumeros_KeyPress;
            
            // Deshabilitar el botón de reinicio inicialmente
            btnReseat.Enabled = false;
        }
        
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmLineas_Load(object sender, EventArgs e)
        {
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
