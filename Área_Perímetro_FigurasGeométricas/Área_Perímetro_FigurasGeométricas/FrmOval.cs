using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmOval : Form
    {
        Oval objOval = new Oval();

        public FrmOval()
        {
            InitializeComponent();
        }

        private void FrmOval_Load(object sender, EventArgs e)
        {
            objOval.InitializeData(txtMajorSemiaxis, txtMinorSemiaxis, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objOval.ReadData(txtMajorSemiaxis, txtMinorSemiaxis);
            objOval.CalculateArea();
            objOval.CalculatePerimeter();
            objOval.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objOval.InitializeData(txtMajorSemiaxis, txtMinorSemiaxis, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objOval.CloseForm(this);
        }
    }
}
