using Graficar_lineas.Controllers;
using Graficar_lineas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmCurvaBézier : Form
    {
        private CurvaBézierController controlador;
        private Bitmap bitmap;
        private Color colorActual = Color.Black;
        private bool modoSeleccionPuntos = false;
        private List<PointF> puntosBezier = new List<PointF>();
        private bool isAnimating = false;

        public FrmCurvaBézier()
        {
            InitializeComponent();
            InicializarComponentes();
            AsignarManejadoresEventos();
        }

        private void InicializarComponentes()
        {
            // Crear el bitmap con el mismo tamaño del PictureBox
            bitmap = new Bitmap(pictuCanva.Width, pictuCanva.Height);
            
            // Inicializar el controlador con el PictureBox y el bitmap
            controlador = new CurvaBézierController(pictuCanva, bitmap);
            
            // Configurar el color inicial
            pic_color.BackColor = colorActual;
            
            // Configurar el PictureBox
            pictuCanva.Image = bitmap;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            
            // El TextBox de número de puntos inicia vacío
        }

        private void AsignarManejadoresEventos()
        {
            // Asignar manejadores de eventos a los controles
            BtnBezier.Click += BtnBezier_Click;
            BtnStartAnimation.Click += BtnStartAnimation_Click;
            BtnColorSet.Click += BtnColorSet_Click;
            btnLimpiarTodo.Click += BtnLimpiarTodo_Click;
            pictuCanva.MouseClick += PictuCanva_MouseClick;
            
            // Configurar valores por defecto
            txtNumerodePuntos.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        private void BtnBezier_Click(object sender, EventArgs e)
        {
            if (isAnimating) return;
            
            if (int.TryParse(txtNumerodePuntos.Text, out int numPuntos) && numPuntos >= 2)
            {
                // Limpiar puntos anteriores
                puntosBezier.Clear();
                controlador.Limpiar();
                
                modoSeleccionPuntos = true;
                BtnBezier.Enabled = false;
                BtnStartAnimation.Enabled = false;
                
                MessageBox.Show($"Haz clic en el área de dibujo para agregar {numPuntos} puntos.", 
                    "Instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido (mínimo 2).", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnStartAnimation_Click(object sender, EventArgs e)
        {
            if (puntosBezier.Count < 2)
            {
                MessageBox.Show("Primero debes agregar puntos a la curva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isAnimating) return;
            
            try
            {
                isAnimating = true;
                // Deshabilitar botones durante la animación
                BtnBezier.Enabled = false;
                BtnStartAnimation.Enabled = false;
                btnLimpiarTodo.Enabled = false;
                BtnColorSet.Enabled = false;
                txtNumerodePuntos.Enabled = false;

                // Iniciar la animación con más pasos para que sea más suave
                await controlador.AnimarCurva(200, 20);
            }
            finally
            {
                // Volver a habilitar los botones
                BtnBezier.Enabled = true;
                BtnStartAnimation.Enabled = true;
                btnLimpiarTodo.Enabled = true;
                BtnColorSet.Enabled = true;
                txtNumerodePuntos.Enabled = true;
                isAnimating = false;
            }
        }

        private void BtnColorSet_Click(object sender, EventArgs e)
        {
            if (isAnimating) return;
            
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    colorActual = colorDialog.Color;
                    pic_color.BackColor = colorActual;
                    controlador.ActualizarColor(colorActual);
                }
            }
        }

        private void BtnLimpiarTodo_Click(object sender, EventArgs e)
        {
            if (isAnimating) return;
            
            // Limpiar el área de dibujo
            controlador.Limpiar();
            
            // Reiniciar variables y controles
            puntosBezier.Clear();
            modoSeleccionPuntos = false;
            txtNumerodePuntos.Clear(); // Limpiar el TextBox
            BtnBezier.Enabled = true;
            BtnStartAnimation.Enabled = false; // Deshabilitar el botón de animación hasta que se agreguen nuevos puntos
        }

        private void PictuCanva_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAnimating || !modoSeleccionPuntos) return;

            // Agregar el punto a la lista y al controlador
            puntosBezier.Add(e.Location);
            controlador.AgregarPunto(e.Location);
            
            // Verificar si ya se han agregado todos los puntos necesarios
            if (puntosBezier.Count >= int.Parse(txtNumerodePuntos.Text))
            {
                modoSeleccionPuntos = false;
                BtnBezier.Enabled = true;
                BtnStartAnimation.Enabled = true;
                
                // Dibujar la curva inicial
                controlador.Redibujar();
                
                MessageBox.Show($"Se han agregado {puntosBezier.Count} puntos. Haz clic en 'Animar' para ver la curva.", 
                    "Puntos agregados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCurvaBézier_Load(object sender, EventArgs e)
        {

        }
    }
}
