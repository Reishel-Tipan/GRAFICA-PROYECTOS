using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmTriangle : Form
    {
        Triangle objTriangle = new Triangle();

        public FrmTriangle()
        {
            InitializeComponent();
        }

        private void FrmTriangle_Load(object sender, EventArgs e)
        {
            objTriangle.InitializeData(txtSide1, txtSide2, txtSide3, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objTriangle.ReadData(txtSide1, txtSide2, txtSide3);
            objTriangle.CalculateArea();
            objTriangle.CalculatePerimeter();
            objTriangle.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objTriangle.InitializeData(txtSide1, txtSide2, txtSide3, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objTriangle.CloseForm(this);
        }
    }
}
