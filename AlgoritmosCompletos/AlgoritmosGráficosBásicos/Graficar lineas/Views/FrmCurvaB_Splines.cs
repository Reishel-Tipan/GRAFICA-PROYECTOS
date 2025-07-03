using Graficar_lineas.Controllers;
using Graficar_lineas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmCurvaB_Splines : Form
    {
        private CurvaB_SplinesController controlador;
        private Bitmap bitmap;
        private Color colorActual = Color.Black;
        private bool modoSeleccionPuntos = false;
        private List<PointF> puntosSpline = new List<PointF>();
        private bool isAnimating = false;

        public FrmCurvaB_Splines()
        {
            InitializeComponent();
            InicializarComponentes();
            AsignarManejadoresEventos();
        }

        private void InicializarComponentes()
        {
            bitmap = new Bitmap(pictuCanva.Width, pictuCanva.Height);
            controlador = new CurvaB_SplinesController(pictuCanva, bitmap);
            pic_color.BackColor = colorActual;
            pictuCanva.Image = bitmap;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
        }

        private void AsignarManejadoresEventos()
        {
            BtnSpline.Click += BtnSpline_Click;
            BtnStartAnimation.Click += BtnStartAnimation_Click;
            BtnColorSet.Click += BtnColorSet_Click;
            btnLimpiarTodo.Click += BtnLimpiarTodo_Click;
            pictuCanva.MouseClick += PictuCanva_MouseClick;

            txtNumerodePuntos.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };

            // Inicialmente deshabilitar el botón de animación hasta que haya puntos
            BtnStartAnimation.Enabled = false;
        }

        private void BtnSpline_Click(object sender, EventArgs e)
        {
            if (isAnimating) return;

            if (int.TryParse(txtNumerodePuntos.Text, out int numPuntos) && numPuntos >= 4)
            {
                puntosSpline.Clear();
                controlador.Limpiar();

                modoSeleccionPuntos = true;
                BtnSpline.Enabled = false;
                BtnStartAnimation.Enabled = false;

                MessageBox.Show($"Haz clic en el área de dibujo para agregar {numPuntos} puntos.",
                    "Instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Se necesitan al menos 4 puntos para B-Splines.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnStartAnimation_Click(object sender, EventArgs e)
        {
            if (puntosSpline.Count < 4)
            {
                MessageBox.Show("Primero debes agregar al menos 4 puntos para la curva B-Spline.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BtnStartAnimation.Enabled = false;
            BtnSpline.Enabled = false;

            isAnimating = true;

            await controlador.AnimarCurvaBSpline(puntosSpline, 200, 20);

            isAnimating = false;

            BtnStartAnimation.Enabled = true;
            BtnSpline.Enabled = true;
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

            controlador.Limpiar();
            puntosSpline.Clear();
            modoSeleccionPuntos = false;
            txtNumerodePuntos.Clear();
            BtnSpline.Enabled = true;
            BtnStartAnimation.Enabled = false;
        }

        private void PictuCanva_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAnimating || !modoSeleccionPuntos) return;

            puntosSpline.Add(e.Location);
            controlador.AgregarPunto(e.Location);

            if (puntosSpline.Count >= int.Parse(txtNumerodePuntos.Text))
            {
                modoSeleccionPuntos = false;
                BtnSpline.Enabled = true;
                BtnStartAnimation.Enabled = true;

                controlador.Redibujar(puntosSpline);

                MessageBox.Show($"Se agregaron {puntosSpline.Count} puntos. Haz clic en 'Animar'.",
                    "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close(); // Se mostrará FrmHome automáticamente
        }
    }
}
