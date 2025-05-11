using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmKite : Form
    {
        private Kite objKite = new Kite();

        public FrmKite()
        {
            InitializeComponent();
        }

        private void FrmKite_Load(object sender, EventArgs e)
        {
            objKite.InitializeData(txtMajorDiagonal, txtMinorDiagonal, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objKite.ReadData(txtMajorDiagonal, txtMinorDiagonal, txtHeight, txtSide);
            objKite.CalculatePerimeter();
            objKite.CalculateArea();
            objKite.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objKite.InitializeData(txtMajorDiagonal, txtMinorDiagonal, txtHeight, txtSide, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objKite.CloseForm(this);
        }
    }
}
