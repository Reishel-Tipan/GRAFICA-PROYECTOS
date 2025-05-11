using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmCircle : Form
    {
        private Circle objCircle = new Circle();

        public FrmCircle()
        {
            InitializeComponent();
        }

        private void FrmCircle_Load(object sender, EventArgs e)
        {
            objCircle.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            objCircle.ReadData(txtRadius);
            objCircle.CalculatePerimeter();
            objCircle.CalculateArea();
            objCircle.PrintData(txtPerimeter, txtArea);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objCircle.InitializeData(txtRadius, txtPerimeter, txtArea);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objCircle.CloseForm(this);
        }

        private void txtArea_TextChanged(object sender, EventArgs e)
        {
            // lógica opcional aquí
        }

        private void txtPerimeter_TextChanged(object sender, EventArgs e)
        {
            // lógica opcional aquí
        }


        // Los demás eventos no son necesarios para lógica, puedes eliminarlos si no los usas
    }
}
