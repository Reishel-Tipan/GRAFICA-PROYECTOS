using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public partial class FrmBresenham : Form
    {
        public FrmBresenham()
        {
            InitializeComponent();
            
            // Configurar el formulario como ventana de herramientas con borde fijo
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Algoritmo de Bresenham";
        }

        private Bresenham brese;
        private void btnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos tengan valores
                if (string.IsNullOrWhiteSpace(txtX_1.Text) || string.IsNullOrWhiteSpace(txtX_2.Text) || 
                    string.IsNullOrWhiteSpace(txtY_1.Text) || string.IsNullOrWhiteSpace(txtY_2.Text))
                {
                    MessageBox.Show("Por favor ingrese todos los valores.", "Datos incompletos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convertir y validar que sean números positivos
                int x1, x2, y1, y2;
                if (!int.TryParse(txtX_1.Text, out x1) || x1 < 0 ||
                    !int.TryParse(txtX_2.Text, out x2) || x2 < 0 ||
                    !int.TryParse(txtY_1.Text, out y1) || y1 < 0 ||
                    !int.TryParse(txtY_2.Text, out y2) || y2 < 0)
                {
                    MessageBox.Show("Por favor ingrese solo números enteros positivos.", "Datos inválidos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Limpiar el ListBox
                listBoxPuntos.Items.Clear();
                
                // Crear una instancia de la clase Bresenham, pasando el PictureBox
                brese = new Bresenham(picDibujo);
                
                // Suscribirse al evento de actualización de puntos
                brese.PuntosActualizados += (s, args) => 
                {
                    // Actualizar la lista de puntos en el hilo de la interfaz de usuario
                    if (listBoxPuntos.InvokeRequired)
                    {
                        listBoxPuntos.Invoke(new Action(MostrarPuntosEnLista));
                    }
                    else
                    {
                        MostrarPuntosEnLista();
                    }
                };
                
                // Establecer los valores validados
                brese.SetPoints(x1, y1, x2, y2);
                
                // Graficar la línea
                brese.PlotShape(picDibujo);
                
                // Mostrar los puntos en el ListBox
                MostrarPuntosEnLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al graficar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void MostrarPuntosEnLista()
        {
            if (brese == null || brese.PuntosDibujados == null || brese.PuntosDibujados.Count == 0)
                return;
                
            // Usar BeginUpdate/EndUpdate para mejorar el rendimiento
            listBoxPuntos.BeginUpdate();
            try
            {
                // Limpiar el ListBox
                listBoxPuntos.Items.Clear();
                
                // Limpiar y actualizar el ListBox
                listBoxPuntos.Items.Clear();
                
                // Obtener los puntos ordenados directamente del método
                var puntosOrdenados = brese.ObtenerPuntosOrdenados();
                
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

        private void FrmBresenham_Load(object sender, EventArgs e)
        {
            // Configuración inicial de los TextBox
            txtX_1.KeyPress += TextBox_KeyPress;
            txtX_2.KeyPress += TextBox_KeyPress;
            txtY_1.KeyPress += TextBox_KeyPress;
            txtY_2.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                
                // Limpiar el ListBox de puntos
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
                
                // Detener cualquier animación en curso
                if (brese != null)
                {
                    // Si la clase Bresenham tiene un método para detener la animación, llamarlo aquí
                    // brese.DetenerAnimacion();
                    
                    // Liberar recursos si es necesario
                    var temp = brese;
                    brese = null;
                    
                    // Forzar recolección de basura para liberar recursos
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                
                // Establecer el foco en el primer campo
                txtX_1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
