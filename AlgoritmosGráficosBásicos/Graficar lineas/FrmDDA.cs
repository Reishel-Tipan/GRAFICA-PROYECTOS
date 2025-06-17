using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graficar_lineas
{

    public partial class FrmLineas : Form
    { 
        private Graficar graficar;

        public FrmLineas()
        {
            InitializeComponent();
            
            // Configurar el formulario como ventana de herramientas con borde fijo
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Configurar validación de entrada
            ConfigurarValidacionEntrada();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void labelx1_Click(object sender, EventArgs e)
        {

        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear una nueva instancia de la clase Graficar
                graficar = new Graficar(picDibujo);
                
                // Suscribirse al evento de actualización de puntos
                graficar.PuntosActualizados += (s, args) =>
                {
                    if (listBoxPuntos.InvokeRequired)
                    {
                        listBoxPuntos.Invoke(new Action(ActualizarListaPuntos));
                    }
                    else
                    {
                        ActualizarListaPuntos();
                    }
                };

                // Leer los datos y graficar
                if (graficar.ReadData(txtX_1, txtX_2, txtY_1, txtY_2))
                {
                    graficar.PlotShape(picDibujo);
                    // Habilitar el botón de reinicio
                    btnReseat.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al graficar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void picCanvas2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnReseat_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar los campos de texto
                txtX_1.Clear();
                txtY_1.Clear();
                txtX_2.Clear();
                txtY_2.Clear();
                
                // Limpiar el ListBox
                listBoxPuntos.Items.Clear();
                
                // Limpiar el PictureBox
                if (picDibujo.Image != null)
                {
                    using (var g = Graphics.FromImage(picDibujo.Image))
                    {
                        g.Clear(picDibujo.BackColor);
                    }
                    picDibujo.Invalidate();
                }
                
                // Deshabilitar el botón de reinicio
                btnReseat.Enabled = false;
                
                // Establecer el foco en el primer campo
                txtX_1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ActualizarListaPuntos()
        {
            if (graficar == null || graficar.PuntosDibujados == null) return;
            
            listBoxPuntos.BeginUpdate();
            try
            {
                listBoxPuntos.Items.Clear();
                
                // Obtener los puntos ordenados
                var puntosOrdenados = graficar.ObtenerPuntosOrdenados();
                
                // Agregar los puntos al ListBox
                foreach (var punto in puntosOrdenados)
                {
                    listBoxPuntos.Items.Add($"({punto.X}, {punto.Y})");
                }
            }
            finally
            {
                listBoxPuntos.EndUpdate();
            }
        }
        
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
    }
}
