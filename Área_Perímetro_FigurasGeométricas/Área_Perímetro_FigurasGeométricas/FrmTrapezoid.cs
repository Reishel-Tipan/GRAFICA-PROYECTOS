using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmTrapezoid : Form
    {
        Trapezoid objTrapezoid = new Trapezoid();

        public FrmTrapezoid()
        {
            InitializeComponent();
        }

        private void FrmTrapezoid_Load(object sender, EventArgs e)
        {
            objTrapezoid.InitializeData(txtSide1, txtSide2, txtSide3, txtSide4, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objTrapezoid.ReadData(txtSide1, txtSide2, txtSide3, txtSide4);
            objTrapezoid.CalculateArea();
            objTrapezoid.CalculatePerimeter();
            objTrapezoid.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objTrapezoid.InitializeData(txtSide1, txtSide2, txtSide3, txtSide4, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objTrapezoid.CloseForm(this);
        }
    }
}
