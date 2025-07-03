using Graficar_lineas.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmCurvaB_Splines : Form
    {
        private CurvaB_SplineController controlador;
        private bool modoSeleccionPuntos = false;
        private ColorDialog colorDialog = new ColorDialog();
        private Color colorActual = Color.Black;
        private int grosorActual = 1;

        public FrmCurvaB_Splines()
        {
            InitializeComponent();
            InicializarComponentes();
            AsignarManejadoresEventos();
            
            // Inicializar el controlador
            controlador = new CurvaB_SplineController(pictuCanva);
            
            // Configurar el color inicial
            pic_color.BackColor = colorActual;
            
            // Configurar el valor inicial del grado
            numericUpDown1.Minimum = 2;
            numericUpDown1.Maximum = 5;
            numericUpDown1.Value = 2;
        }

        private void InicializarComponentes()
        {
            // Inicializar el PictureBox
            pictuCanva.Image = new Bitmap(pictuCanva.Width, pictuCanva.Height);
            using (Graphics g = Graphics.FromImage(pictuCanva.Image))
            {
                g.Clear(Color.White);
            }
        }

        private void AsignarManejadoresEventos()
        {
            // Eventos de los botones
            BtnBezier.Click += BtnBezier_Click;
            BtnStartAnimation.Click += BtnStartAnimation_Click;
            BtnColorSet.Click += BtnColorSet_Click;
            btnLimpiarTodo.Click += BtnLimpiarTodo_Click;
            
            // Evento del PictureBox
            pictuCanva.MouseClick += PictuCanva_MouseClick;
            
            // Evento para actualizar el grado de la curva
            numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;
        }

        private void BtnBezier_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumerodePuntos.Text))
            {
                MessageBox.Show("Ingrese el número de puntos de control.", "Advertencia", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtNumerodePuntos.Text, out int numPuntos) || numPuntos < 2)
            {
                MessageBox.Show("El número de puntos debe ser un entero mayor o igual a 2.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el grado seleccionado
            int grado = (int)numericUpDown1.Value;
            
            if (numPuntos <= grado)
            {
                MessageBox.Show($"El número de puntos debe ser mayor que el grado de la curva (actual: {grado}).", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Limpiar la selección anterior
            controlador.LimpiarTodo();
            
            // Iniciar la selección de puntos
            modoSeleccionPuntos = true;
            controlador.IniciarSeleccionPuntos(numPuntos);
            
            MessageBox.Show($"Haz clic en el área de dibujo para agregar {numPuntos} puntos de control.", 
                "Selección de puntos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnStartAnimation_Click(object sender, EventArgs e)
        {
            await controlador.AnimarCurva();
        }

        private void BtnColorSet_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorActual = colorDialog.Color;
                pic_color.BackColor = colorActual;
                controlador.EstablecerColor(colorActual);
                
                // Redibujar la curva si hay puntos
                controlador.DibujarCurva();
            }
        }

        private void BtnLimpiarTodo_Click(object sender, EventArgs e)
        {
            controlador.LimpiarTodo();
            txtNumerodePuntos.Clear();
            modoSeleccionPuntos = false;
        }

        private void PictuCanva_MouseClick(object sender, MouseEventArgs e)
        {
            if (modoSeleccionPuntos)
            {
                controlador.AgregarPunto(e.Location);
            }
        }
        
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int nuevoGrado = (int)numericUpDown1.Value;
            controlador.ActualizarGrado(nuevoGrado);
        }

        private void FrmCurvaB_Splines_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Liberar recursos
            controlador.LimpiarTodo();
        }
    }
}
