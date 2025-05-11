using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Square
    {
        private float side, area, perimeter;

        public void ReadData(TextBox txtSide)
        {
            string input = txtSide.Text.Trim();

            if (!float.TryParse(input, out side) || side <= 0)
            {
                MessageBox.Show("Ingrese un valor válido, positivo y mayor a cero para el lado.", "Error");
                side = 0;
            }
        }

        public void CalculateArea()
        {
            area = side * side;
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

        public void InitializeData(TextBox txtSide, TextBox txtPerimeter, TextBox txtArea)
        {
            side = area = perimeter = 0;
            txtSide.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtSide.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
