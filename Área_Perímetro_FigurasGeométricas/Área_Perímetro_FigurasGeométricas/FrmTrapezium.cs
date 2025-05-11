using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmTrapezium : Form
    {
        Trapezium objTrapezium = new Trapezium();

        public FrmTrapezium()
        {
            InitializeComponent();
        }

        private void FrmTrapezium_Load(object sender, EventArgs e)
        {
            objTrapezium.InitializeData(txtMajorBase, txtMinorBase, txtHeight, txtSide1, txtSide2, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objTrapezium.ReadData(txtMajorBase, txtMinorBase, txtHeight, txtSide1, txtSide2);
            objTrapezium.CalculateArea();
            objTrapezium.CalculatePerimeter();
            objTrapezium.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objTrapezium.InitializeData(txtMajorBase, txtMinorBase, txtHeight, txtSide1, txtSide2, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objTrapezium.CloseForm(this);
        }
    }
}
