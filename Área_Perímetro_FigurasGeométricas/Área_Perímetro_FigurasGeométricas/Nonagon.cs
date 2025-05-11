using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Nonagon
    {
        private float radius;
        private float perimeter;
        private float area;

        public Nonagon()
        {
            radius = perimeter = area = 0;
        }

        public void ReadData(TextBox txtRadius)
        {
            string input = txtRadius.Text.Trim();

            if (!float.TryParse(input, out radius) || radius <= 0)
            {
                MessageBox.Show("Ingrese un valor numérico, positivo y mayor a cero para el radio.", "Error");
                radius = 0;
            }
        }

        public void CalculatePerimeter()
        {
            float side = 2 * radius * (float)Math.Sin(Math.PI / 9);
            perimeter = 9 * side;
        }

        public void CalculateArea()
        {
            area = (9.0f / 2.0f) * radius * radius * (float)Math.Sin(2 * Math.PI / 9);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtRadius, TextBox txtPerimeter, TextBox txtArea)
        {
            radius = perimeter = area = 0;
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
