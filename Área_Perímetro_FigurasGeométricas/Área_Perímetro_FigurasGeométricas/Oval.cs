using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Oval
    {
        private float a; // Semieje mayor
        private float b; // Semieje menor
        private float area;
        private float perimeter;

        public void ReadData(TextBox txtA, TextBox txtB)
        {
            string inputA = txtA.Text.Trim();
            string inputB = txtB.Text.Trim();

            if (!float.TryParse(inputA, out a) || a <= 0 ||
                !float.TryParse(inputB, out b) || b <= 0)
            {
                MessageBox.Show("Ingrese solo números válidos, positivos y mayores que cero para ambos semiejes.", "Error");
                a = b = 0;
            }
        }

        public void CalculateArea()
        {
            area = (float)Math.PI * a * b;
        }

        public void CalculatePerimeter()
        {
            // Fórmula de Ramanujan para aproximar el perímetro de una elipse
            perimeter = (float)(Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b))));
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtA, TextBox txtB, TextBox txtPerimeter, TextBox txtArea)
        {
            a = b = area = perimeter = 0;
            txtA.Text = "";
            txtB.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";
            txtA.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
