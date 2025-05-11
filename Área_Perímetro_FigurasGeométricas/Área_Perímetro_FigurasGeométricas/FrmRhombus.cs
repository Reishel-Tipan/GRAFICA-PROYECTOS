using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmRhombus : Form
    {
        Rhombus objRhombus = new Rhombus();

        public FrmRhombus()
        {
            InitializeComponent();
        }

        private void FrmRhombus_Load(object sender, EventArgs e)
        {
            objRhombus.InitializeData(txtMajorDiagonal, txtMinorDiagonal, txtSide, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objRhombus.ReadData(txtMajorDiagonal, txtMinorDiagonal, txtSide);
            objRhombus.CalculateArea();
            objRhombus.CalculatePerimeter();
            objRhombus.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objRhombus.InitializeData(txtMajorDiagonal, txtMinorDiagonal, txtSide, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objRhombus.CloseForm(this);
        }
    }
}
