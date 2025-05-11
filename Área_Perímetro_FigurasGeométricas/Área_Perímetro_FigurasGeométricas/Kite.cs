using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Kite
    {
        private float majorDiagonal;
        private float minorDiagonal;
        private float height;
        private float side;
        private float perimeter;
        private float area;

        public Kite()
        {
            majorDiagonal = 0;
            minorDiagonal = 0;
            height = 0;
            side = 0;
            perimeter = 0;
            area = 0;
        }

        public void ReadData(TextBox txtMajorDiagonal, TextBox txtMinorDiagonal, TextBox txtHeight, TextBox txtSide)
        {
            string majorDia = txtMajorDiagonal.Text.Trim();
            string minorDia = txtMinorDiagonal.Text.Trim();
            string h = txtHeight.Text.Trim();
            string s = txtSide.Text.Trim();

            // Validación de entradas
            if (!float.TryParse(majorDia, out majorDiagonal) || majorDiagonal <= 0 ||
                !float.TryParse(minorDia, out minorDiagonal) || minorDiagonal <= 0 ||
                !float.TryParse(h, out height) || height <= 0 ||
                !float.TryParse(s, out side) || side <= 0)
            {
                MessageBox.Show("Todos los valores deben ser numéricos, positivos y mayores que cero.", "Error de entrada");
                majorDiagonal = minorDiagonal = height = side = 0;
            }
        }

        public void CalculatePerimeter()
        {
            perimeter = 2 * side + 2 * side;  // Suma de los 4 lados del romboide (2 lados iguales)
        }

        public void CalculateArea()
        {
            area = (majorDiagonal * minorDiagonal) / 2;  // Fórmula para el área de un rombo (cometa)
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtMajorDiagonal, TextBox txtMinorDiagonal, TextBox txtHeight, TextBox txtSide, TextBox txtPerimeter, TextBox txtArea)
        {
            majorDiagonal = minorDiagonal = height = side = perimeter = area = 0;
            txtMajorDiagonal.Text = "";
            txtMinorDiagonal.Text = "";
            txtHeight.Text = "";
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
