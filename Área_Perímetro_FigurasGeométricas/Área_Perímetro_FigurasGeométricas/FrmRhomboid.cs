using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmRhomboid : Form
    {
        Rhomboid objRhomboid = new Rhomboid();

        public FrmRhomboid()
        {
            InitializeComponent();
        }

        private void FrmRhomboid_Load(object sender, EventArgs e)
        {
            objRhomboid.InitializeData(txtBase, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objRhomboid.ReadData(txtBase, txtHeight, txtSide);
            objRhomboid.CalculateArea();
            objRhomboid.CalculatePerimeter();
            objRhomboid.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objRhomboid.InitializeData(txtBase, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objRhomboid.CloseForm(this);
        }
    }
}
