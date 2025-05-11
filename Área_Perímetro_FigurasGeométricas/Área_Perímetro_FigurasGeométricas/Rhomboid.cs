using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Rhomboid
    {
        private float baseR, height, side, area, perimeter;

        public void ReadData(TextBox txtBase, TextBox txtHeight, TextBox txtSide)
        {
            string inputBase = txtBase.Text.Trim();
            string inputHeight = txtHeight.Text.Trim();
            string inputSide = txtSide.Text.Trim();

            if (!float.TryParse(inputBase, out baseR) || baseR <= 0 ||
                !float.TryParse(inputHeight, out height) || height <= 0 ||
                !float.TryParse(inputSide, out side) || side <= 0)
            {
                MessageBox.Show("Ingrese valores válidos, positivos y mayores a cero para base, altura y lado.", "Error");
                baseR = height = side = 0;
            }
        }

        public void CalculateArea()
        {
            area = baseR * height;
        }

        public void CalculatePerimeter()
        {
            perimeter = 2 * (baseR + side);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtBase, TextBox txtHeight, TextBox txtSide, TextBox txtPerimeter, TextBox txtArea)
        {
            baseR = height = side = area = perimeter = 0;
            txtBase.Text = "";
            txtHeight.Text = "";
            txtSide.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtBase.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
