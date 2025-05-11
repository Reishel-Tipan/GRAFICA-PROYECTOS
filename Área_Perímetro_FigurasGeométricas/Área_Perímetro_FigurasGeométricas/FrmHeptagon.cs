using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmHeptagon : Form
    {
        // Instancia de la clase Heptagon
        private Heptagon objHeptagon = new Heptagon();

        public FrmHeptagon()
        {
            InitializeComponent();
        }

        private void FrmHeptagon_Load(object sender, EventArgs e)
        {
            // Limpia los campos al cargar
            objHeptagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objHeptagon.ReadData(txtRadius);
            objHeptagon.CalculatePerimeter();
            objHeptagon.CalculateArea();
            objHeptagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objHeptagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objHeptagon.CloseForm(this);
        }
    }
}
