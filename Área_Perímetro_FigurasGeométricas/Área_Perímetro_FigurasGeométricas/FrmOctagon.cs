using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmOctagon : Form
    {
        Octagon objOctagon = new Octagon();

        public FrmOctagon()
        {
            InitializeComponent();
        }

        private void FrmOctagon_Load(object sender, EventArgs e)
        {
            objOctagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objOctagon.ReadData(txtRadius);
            objOctagon.CalculatePerimeter();
            objOctagon.CalculateArea();
            objOctagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objOctagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objOctagon.CloseForm(this);
        }
    }
}
