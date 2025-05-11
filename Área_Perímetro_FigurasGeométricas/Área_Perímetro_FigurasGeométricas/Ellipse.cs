using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Ellipse
    {
        private float a;  // semieje mayor
        private float b;  // semieje menor
        private float perimeter;
        private float area;

        // Constructor
        public Ellipse()
        {
            a = 0.0f;
            b = 0.0f;
            perimeter = 0.0f;
            area = 0.0f;
        }

        public void ReadData(TextBox txtMajorSemiaxis, TextBox txtMinorSemiaxis)
        {
            try
            {
                a = float.Parse(txtMajorSemiaxis.Text);
                b = float.Parse(txtMinorSemiaxis.Text);

                if (a <= 0 || b <= 0)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Ingrese solo números positivos válidos en ambos semiejes.", "Error");
            }
        }

        public void CalculatePerimeter()
        {
            // Aproximación de Ramanujan para el perímetro de una elipse
            perimeter = (float)(Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b))));
        }

        public void CalculateArea()
        {
            area = (float)(Math.PI * a * b);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtMajorSemiaxis, TextBox txtMinorSemiaxis, TextBox txtPerimeter, TextBox txtArea)
        {
            a = 0.0f;
            b = 0.0f;
            perimeter = 0.0f;
            area = 0.0f;

            txtMajorSemiaxis.Text = "";
            txtMinorSemiaxis.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";

            txtMajorSemiaxis.Focus();
        }

        public void CloseForm(Form frm)
        {
            frm.Close();
        }
    }
}
