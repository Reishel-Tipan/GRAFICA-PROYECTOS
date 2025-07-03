using System;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas
{
    public partial class FrmCircunferencia : Form
    {
        private readonly CircunferenciaController _controller;

        public FrmCircunferencia()
        {
            InitializeComponent();
            
            // Configurar el formulario como ventana de herramientas con borde fijo
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Algoritmo de Circunferencia";
            
            // Inicializar el controlador
            _controller = new CircunferenciaController(
                picDibujo,
                listBoxPuntos,
                txtX_1, txtY_1, txtRadio,
                btnGraficar, btnReseat);
                
            // Configurar validación de entrada
            ConfigurarValidacionEntrada();
        }
        
        private void ConfigurarValidacionEntrada()
        {
            // Configurar el evento KeyPress para todos los TextBox
            txtX_1.KeyPress += TextBox_KeyPress;
            txtY_1.KeyPress += TextBox_KeyPress;
            txtRadio.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos, signo negativo (solo al inicio) y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && 
                (e.KeyChar != '-' || ((TextBox)sender).Text.Length > 0))
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
        
        private void FrmCircunferencia_Load(object sender, EventArgs e)
        {
            // Inicializar el ListBox si es necesario
            if (listBoxPuntos == null)
            {
                listBoxPuntos = new ListBox();
                listBoxPuntos.Dock = DockStyle.Fill;
                groupBox3.Controls.Add(listBoxPuntos);
            }
        }
        
        private void FrmCircunferencia_FormClosing(object sender, FormClosingEventArgs e)
        {
            // No es necesario liberar recursos aquí ya que el controlador se encarga de eso
            // El garbage collector se encargará de liberar el controlador cuando el formulario se cierre
        }
    }
}
