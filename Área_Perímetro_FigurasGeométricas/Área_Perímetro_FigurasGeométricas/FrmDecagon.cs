using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmDecagon : Form
    {
        private Decagon objDecagon = new Decagon();

        public FrmDecagon()
        {
            InitializeComponent();
        }

        private void FrmDecagon_Load(object sender, EventArgs e)
        {
            objDecagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void txtRadius_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objDecagon.ReadData(txtRadius);
            objDecagon.Perimeter();
            objDecagon.Area();
            objDecagon.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objDecagon.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objDecagon.CloseForm(this);
        }

        private void txtPerimeter_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtArea_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
