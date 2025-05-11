using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmPentagon : Form
    {
        Pentagon objPentagon = new Pentagon();

        public FrmPentagon()
        {
            InitializeComponent();
        }

        private void FrmPentagon_Load(object sender, EventArgs e)
        {
            objPentagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objPentagon.ReadData(txtRadius);
            objPentagon.CalculateArea();
            objPentagon.CalculatePerimeter();
            objPentagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objPentagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objPentagon.CloseForm(this);
        }
    }
}
