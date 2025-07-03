using System;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas
{
    public partial class FrmBresenham : Form
    {
        private readonly BresenhamController _controller;

        public FrmBresenham()
        {
            InitializeComponent();
            
            // Configurar el formulario como ventana de herramientas con borde fijo
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Algoritmo de Bresenham";
            
            // Inicializar el controlador
            _controller = new BresenhamController(
                picDibujo,
                listBoxPuntos,
                txtX_1, txtY_1,
                txtX_2, txtY_2,
                btnGraficar, btnReseat);
                
            // Configurar validación de entrada
            ConfigurarValidacionEntrada();
        }
        
        private void ConfigurarValidacionEntrada()
        {
            // Configurar el evento KeyPress para todos los TextBox
            txtX_1.KeyPress += TextBox_KeyPress;
            txtX_2.KeyPress += TextBox_KeyPress;
            txtY_1.KeyPress += TextBox_KeyPress;
            txtY_2.KeyPress += TextBox_KeyPress;
        }
        
        private void FrmBresenham_Load(object sender, EventArgs e)
        {
            // Configuración adicional al cargar el formulario
            // Por ejemplo, establecer el foco en el primer campo
            txtX_1.Focus();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        // Los manejadores de eventos ahora están en el controlador
        
        private void BtnGraficar_Click(object sender, EventArgs e)
        {
            _controller.BtnGraficar_Click(sender, e);
        }

        private void BtnReseat_Click(object sender, EventArgs e)
        {
            _controller.BtnReseat_Click(sender, e);
        }
    }
}
