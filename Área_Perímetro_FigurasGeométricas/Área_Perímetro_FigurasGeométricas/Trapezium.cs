using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Trapezium
    {
        private float majorBase, minorBase, height, side1, side2, area, perimeter;

        public void ReadData(TextBox txtMajorBase, TextBox txtMinorBase, TextBox txtHeight, TextBox txtSide1, TextBox txtSide2)
        {
            bool valid = true;

            valid &= float.TryParse(txtMajorBase.Text.Trim(), out majorBase) && majorBase > 0;
            valid &= float.TryParse(txtMinorBase.Text.Trim(), out minorBase) && minorBase > 0;
            valid &= float.TryParse(txtHeight.Text.Trim(), out height) && height > 0;
            valid &= float.TryParse(txtSide1.Text.Trim(), out side1) && side1 > 0;
            valid &= float.TryParse(txtSide2.Text.Trim(), out side2) && side2 > 0;

            if (!valid)
            {
                MessageBox.Show("Ingrese solo valores numéricos positivos y mayores a cero.", "Error");
                majorBase = minorBase = height = side1 = side2 = 0;
            }
        }

        public void CalculateArea()
        {
            area = ((majorBase + minorBase) * height) / 2;
        }

        public void CalculatePerimeter()
        {
            perimeter = majorBase + minorBase + side1 + side2;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtMajorBase, TextBox txtMinorBase, TextBox txtHeight, TextBox txtSide1, TextBox txtSide2, TextBox txtPerimeter, TextBox txtArea)
        {
            majorBase = minorBase = height = side1 = side2 = area = perimeter = 0;
            txtMajorBase.Text = "";
            txtMinorBase.Text = "";
            txtHeight.Text = "";
            txtSide1.Text = "";
            txtSide2.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtMajorBase.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
