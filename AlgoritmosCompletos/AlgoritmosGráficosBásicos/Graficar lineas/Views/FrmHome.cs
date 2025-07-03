using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Views;

namespace Graficar_lineas
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
            this.Text = "Algoritmos gráficos";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            this.Load += FrmInicio_Load;
        }

        private void AbrirFormulario(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
            this.Hide(); // Oculta FrmHome
            form.FormClosed += (s, args) => this.Show(); // ✅ Al cerrarse el hijo, se vuelve a mostrar Home

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDDA());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmBresenham());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmElipseBresenham());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCircunferencia());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmScanline());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmRellenado());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCurvaB_Splines());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCurvaBézier());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmSutherland_Hodgman());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCohen_Sutherland());
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(0, 160, 255); // Celeste moderno
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;

                    // Opcional: efecto al pasar el mouse
                    btn.MouseEnter += (s, ev) => btn.BackColor = Color.FromArgb(0, 120, 220);
                    btn.MouseLeave += (s, ev) => btn.BackColor = Color.FromArgb(0, 160, 255);
                }
            }
        }

        private IEnumerable<Button> GetAllButtons(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Button btn)
                    yield return btn;

                foreach (var subBtn in GetAllButtons(c))
                    yield return subBtn;
            }
        }


    }
}
