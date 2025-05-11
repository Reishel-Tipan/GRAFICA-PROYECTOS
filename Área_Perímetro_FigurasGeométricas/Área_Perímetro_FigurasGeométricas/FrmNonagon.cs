using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmNonagon : Form
    {
        Nonagon objNonagon = new Nonagon();

        public FrmNonagon()
        {
            InitializeComponent();
        }

        private void FrmNonagon_Load(object sender, EventArgs e)
        {
            objNonagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objNonagon.ReadData(txtRadius);
            objNonagon.CalculatePerimeter();
            objNonagon.CalculateArea();
            objNonagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objNonagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objNonagon.CloseForm(this);
        }
    }
}
