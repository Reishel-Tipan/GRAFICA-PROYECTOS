using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmSquare : Form
    {
        Square objSquare = new Square();

        public FrmSquare()
        {
            InitializeComponent();
        }

        private void FrmSquare_Load(object sender, EventArgs e)
        {
            objSquare.InitializeData(txtSide, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objSquare.ReadData(txtSide);
            objSquare.CalculateArea();
            objSquare.CalculatePerimeter();
            objSquare.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objSquare.InitializeData(txtSide, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objSquare.CloseForm(this);
        }
    }
}
