using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmEllipse : Form
    {
        private Ellipse objEllipse = new Ellipse();

        public FrmEllipse()
        {
            InitializeComponent();
        }

        private void FrmEllipse_Load(object sender, EventArgs e)
        {
            objEllipse.InitializeData(txtMajorSemiaxis, txtMinorSemiaxis, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objEllipse.ReadData(txtMajorSemiaxis, txtMinorSemiaxis);
            objEllipse.CalculatePerimeter();
            objEllipse.CalculateArea();
            objEllipse.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objEllipse.InitializeData(txtMajorSemiaxis, txtMinorSemiaxis, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objEllipse.CloseForm(this);
        }

        private void txtMajorSemiaxis_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMinorSemiaxis_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
