using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmRectangle : Form
    {
        Rectangle objRectangle = new Rectangle();

        public FrmRectangle()
        {
            InitializeComponent();
        }

        private void FrmRectangle_Load(object sender, EventArgs e)
        {
            objRectangle.InitializeData(txtBase, txtHeight, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objRectangle.ReadData(txtBase, txtHeight);
            objRectangle.CalculateArea();
            objRectangle.CalculatePerimeter();
            objRectangle.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objRectangle.InitializeData(txtBase, txtHeight, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objRectangle.CloseForm(this);
        }
    }
}
