using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Pentagon
    {
        private float radius;
        private float side;
        private float perimeter;
        private float area;

        public void ReadData(TextBox txtRadius)
        {
            string input = txtRadius.Text.Trim();

            if (!float.TryParse(input, out radius) || radius <= 0)
            {
                MessageBox.Show("Ingrese un valor numérico válido, positivo y mayor que cero para el radio.", "Error");
                radius = 0;
            }
        }

        public void CalculatePerimeter()
        {
            side = 2 * radius * (float)Math.Sin(Math.PI / 5); // Lado desde el radio
            perimeter = 5 * side;
        }

        public void CalculateArea()
        {
            area = (5f / 2f) * radius * radius * (float)Math.Sin((2 * Math.PI) / 5);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtRadius, TextBox txtPerimeter, TextBox txtArea)
        {
            radius = side = perimeter = area = 0;
            txtRadius.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtRadius.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
