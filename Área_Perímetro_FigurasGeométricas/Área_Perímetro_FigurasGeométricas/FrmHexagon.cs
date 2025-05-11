using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmHexagon : Form
    {
        private Hexagon objHexagon = new Hexagon();

        public FrmHexagon()
        {
            InitializeComponent();
        }

        private void FrmHexagon_Load(object sender, EventArgs e)
        {
            objHexagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objHexagon.ReadData(txtRadius);
            objHexagon.CalculatePerimeter();
            objHexagon.CalculateArea();
            objHexagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objHexagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objHexagon.CloseForm(this);
        }
    }
}
