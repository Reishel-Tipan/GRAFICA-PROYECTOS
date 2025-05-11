using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class IsoscelesTrapezoid
    {
        private float baseMajor;
        private float baseMinor;
        private float height;
        private float side;
        private float perimeter;
        private float area;

        public IsoscelesTrapezoid()
        {
            baseMajor = 0;
            baseMinor = 0;
            height = 0;
            side = 0;
            perimeter = 0;
            area = 0;
        }

        public void ReadData(TextBox txtMajorBase, TextBox txtMinorBase, TextBox txtHeight, TextBox txtSide)
        {
            string bMaj = txtMajorBase.Text.Trim();
            string bMin = txtMinorBase.Text.Trim();
            string h = txtHeight.Text.Trim();
            string l = txtSide.Text.Trim();

            if (!float.TryParse(bMaj, out baseMajor) || baseMajor <= 0 ||
                !float.TryParse(bMin, out baseMinor) || baseMinor <= 0 ||
                !float.TryParse(h, out height) || height <= 0 ||
                !float.TryParse(l, out side) || side <= 0)
            {
                MessageBox.Show("Todos los valores deben ser numéricos, positivos y mayores que cero.", "Error de entrada");
                baseMajor = baseMinor = height = side = 0;
            }
        }

        public void CalculatePerimeter()
        {
            perimeter = baseMajor + baseMinor + 2 * side;
        }

        public void CalculateArea()
        {
            area = ((baseMajor + baseMinor) / 2) * height;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtMajorBase, TextBox txtMinorBase, TextBox txtHeight, TextBox txtSide, TextBox txtPerimeter, TextBox txtArea)
        {
            baseMajor = baseMinor = height = side = perimeter = area = 0;
            txtMajorBase.Text = "";
            txtMinorBase.Text = "";
            txtHeight.Text = "";
            txtSide.Text = "";
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
