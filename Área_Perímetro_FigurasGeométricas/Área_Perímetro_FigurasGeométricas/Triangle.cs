using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Triangle
    {
        private float side1, side2, side3, area, perimeter;

        public void ReadData(TextBox txtSide1, TextBox txtSide2, TextBox txtSide3)
        {
            bool valid = true;

            valid &= float.TryParse(txtSide1.Text.Trim(), out side1) && side1 > 0;
            valid &= float.TryParse(txtSide2.Text.Trim(), out side2) && side2 > 0;
            valid &= float.TryParse(txtSide3.Text.Trim(), out side3) && side3 > 0;

            if (!valid)
            {
                MessageBox.Show("Ingrese solo valores numéricos positivos y mayores a cero.", "Error");
                side1 = side2 = side3 = 0;
            }
        }

        public void CalculateArea()
        {
            // Cálculo del área usando la fórmula de Herón:
            // Semi-perímetro: s = (side1 + side2 + side3) / 2
            // Área = √(s * (s - side1) * (s - side2) * (s - side3))

            float s = (side1 + side2 + side3) / 2;
            area = (float)Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));
        }

        public void CalculatePerimeter()
        {
            // El perímetro de un triángulo es la suma de sus tres lados
            perimeter = side1 + side2 + side3;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtSide1, TextBox txtSide2, TextBox txtSide3, TextBox txtPerimeter, TextBox txtArea)
        {
            side1 = side2 = side3 = area = perimeter = 0;
            txtSide1.Text = "";
            txtSide2.Text = "";
            txtSide3.Text = "";
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
