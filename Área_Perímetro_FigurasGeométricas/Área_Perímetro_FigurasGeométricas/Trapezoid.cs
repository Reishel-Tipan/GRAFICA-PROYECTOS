using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Trapezoid
    {
        private float side1, side2, side3, side4, area, perimeter;

        public void ReadData(TextBox txtSide1, TextBox txtSide2, TextBox txtSide3, TextBox txtSide4)
        {
            bool valid = true;

            valid &= float.TryParse(txtSide1.Text.Trim(), out side1) && side1 > 0;
            valid &= float.TryParse(txtSide2.Text.Trim(), out side2) && side2 > 0;
            valid &= float.TryParse(txtSide3.Text.Trim(), out side3) && side3 > 0;
            valid &= float.TryParse(txtSide4.Text.Trim(), out side4) && side4 > 0;

            if (!valid)
            {
                MessageBox.Show("Ingrese solo valores numéricos positivos y mayores a cero.", "Error");
                side1 = side2 = side3 = side4 = 0;
            }
        }

        public void CalculateArea()
        {
            // El área de un trapecio se calcula como:
            // Área = (b1 + b2) * h / 2
            // Asumimos que se tienen las bases (b1 y b2) y la altura (h) del trapecio
            area = ((side1 + side2) * side3) / 2; // Aquí side1 y side2 se asumen como las bases
        }

        public void CalculatePerimeter()
        {
            // El perímetro de un trapecio es la suma de todos sus lados
            perimeter = side1 + side2 + side3 + side4;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtSide1, TextBox txtSide2, TextBox txtSide3, TextBox txtSide4, TextBox txtPerimeter, TextBox txtArea)
        {
            side1 = side2 = side3 = side4 = area = perimeter = 0;
            txtSide1.Text = "";
            txtSide2.Text = "";
            txtSide3.Text = "";
            txtSide4.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtSide1.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
