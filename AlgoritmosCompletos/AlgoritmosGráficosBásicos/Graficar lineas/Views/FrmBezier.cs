using Graficar_lineas.Controllers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmBezier : Form
    {
        private CurvaBézierController controlador;
        private Bitmap bm;
        private bool dibujando = false;

        public FrmBezier()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Text = "Curva de Bézier";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crear el PictureBox
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pictureBox);

            // Crear botones
            var panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.LightGray
            };
            this.Controls.Add(panelBotones);

            var btnAgregarPunto = new Button { Text = "Agregar Punto", Width = 100 };
            var btnDibujar = new Button { Text = "Dibujar Curva", Width = 100 };
            var btnAnimar = new Button { Text = "Animar", Width = 100 };
            var btnLimpiar = new Button { Text = "Limpiar", Width = 100 };

            // Agregar controles al panel
            panelBotones.Controls.Add(btnAgregarPunto);
            panelBotones.Controls.Add(btnDibujar);
            panelBotones.Controls.Add(btnAnimar);
            panelBotones.Controls.Add(btnLimpiar);

            // Crear bitmap y controlador
            bm = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (var g = Graphics.FromImage(bm))
            {
                g.Clear(Color.White);
            }
            pictureBox.Image = bm;
            controlador = new CurvaBézierController(pictureBox, bm);

            // Eventos
            pictureBox.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && dibujando)
                {
                    controlador.AgregarPunto(e.Location);
                }
            };

            btnAgregarPunto.Click += (s, e) =>
            {
                dibujando = true;
                btnAgregarPunto.Enabled = false;
                btnDibujar.Enabled = true;
            };

            btnDibujar.Click += (s, e) =>
            {
                controlador.DibujarCurva();
                dibujando = false;
                btnAgregarPunto.Enabled = true;
            };

            btnAnimar.Click += async (s, e) =>
            {
                if (controlador is null) return;
                
                btnAnimar.Enabled = false;
                try
                {
                    await controlador.AnimarCurva();
                }
                finally
                {
                    btnAnimar.Enabled = true;
                }
            };

            btnLimpiar.Click += (s, e) =>
            {
                controlador.Limpiar();
                dibujando = false;
                btnAgregarPunto.Enabled = true;
                btnDibujar.Enabled = false;
            };
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            controlador?.Dispose();
            bm?.Dispose();
        }
    }
}
