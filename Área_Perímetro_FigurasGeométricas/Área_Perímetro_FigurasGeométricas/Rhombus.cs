using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Rhombus
    {
        private float majorDiagonal, minorDiagonal, side, area, perimeter;

        public void ReadData(TextBox txtMajorDiagonal, TextBox txtMinorDiagonal, TextBox txtSide)
        {
            string inputMajor = txtMajorDiagonal.Text.Trim();
            string inputMinor = txtMinorDiagonal.Text.Trim();
            string inputSide = txtSide.Text.Trim();

            if (!float.TryParse(inputMajor, out majorDiagonal) || majorDiagonal <= 0 ||
                !float.TryParse(inputMinor, out minorDiagonal) || minorDiagonal <= 0 ||
                !float.TryParse(inputSide, out side) || side <= 0)
            {
                MessageBox.Show("Ingrese valores válidos, positivos y mayores a cero para diagonales y lado.", "Error");
                majorDiagonal = minorDiagonal = side = 0;
            }
        }

        public void CalculateArea()
        {
            area = (majorDiagonal * minorDiagonal) / 2;
        }

        public void CalculatePerimeter()
        {
            perimeter = 4 * side;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtMajorDiagonal, TextBox txtMinorDiagonal, TextBox txtSide, TextBox txtPerimeter, TextBox txtArea)
        {
            majorDiagonal = minorDiagonal = side = area = perimeter = 0;
            txtMajorDiagonal.Text = "";
            txtMinorDiagonal.Text = "";
            txtSide.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtMajorDiagonal.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
