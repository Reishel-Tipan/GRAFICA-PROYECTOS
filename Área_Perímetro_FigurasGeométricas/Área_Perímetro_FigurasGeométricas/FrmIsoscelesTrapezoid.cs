using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmIsoscelesTrapezoid : Form
    {
        private IsoscelesTrapezoid objTrapezoid = new IsoscelesTrapezoid();

        public FrmIsoscelesTrapezoid()
        {
            InitializeComponent();
        }

        private void FrmIsoscelesTrapezoid_Load(object sender, EventArgs e)
        {
            objTrapezoid.InitializeData(txtMajorBase, txtMinorBase, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objTrapezoid.ReadData(txtMajorBase, txtMinorBase, txtHeight, txtSide);
            objTrapezoid.CalculatePerimeter();
            objTrapezoid.CalculateArea();
            objTrapezoid.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objTrapezoid.InitializeData(txtMajorBase, txtMinorBase, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objTrapezoid.CloseForm(this);
        }
    }
}
