using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Circle
    {
        private float radius;
        private float perimeter;
        private float area;

        public void ReadData(TextBox txtRadius)
        {
            try
            {
                radius = float.Parse(txtRadius.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input for radius.", "Error");
            }
        }

        public void CalculatePerimeter()
        {
            perimeter = 2 * (float)Math.PI * radius;
        }

        public void CalculateArea()
        {
            area = (float)Math.PI * radius * radius;
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("0.00");
            txtArea.Text = area.ToString("0.00");
        }

        public void InitializeData(TextBox txtRadius, TextBox txtPerimeter, TextBox txtArea)
        {
            radius = perimeter = area = 0;

            txtRadius.Text = "";
            txtPerimeter.Text = "";
            txtArea.Text = "";

            txtRadius.Focus();
        }

        public void CloseForm(Form form)
        {
            form.Close();
        }
    }
}
