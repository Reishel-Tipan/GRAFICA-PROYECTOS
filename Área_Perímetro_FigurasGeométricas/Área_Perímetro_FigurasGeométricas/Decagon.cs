using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Decagon
    {
        private float mRadius;
        private float mPerimeter;
        private float mArea;

        // Constructor
        public Decagon()
        {
            mRadius = 0.0f;
            mPerimeter = 0.0f;
            mArea = 0.0f;
        }

        // Validación e ingreso del dato
        public void ReadData(TextBox txtRadius)
        {
            string input = txtRadius.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("El campo radio está vacío.", "Error");
                return;
            }

            if (!float.TryParse(input, out mRadius) || mRadius <= 0)
            {
                MessageBox.Show("Ingrese un número válido y positivo.", "Error");
                txtRadius.Clear();
                txtRadius.Focus();
            }
        }

        // Perímetro del decágono regular: P = 2 * n * R * sin(π / n)
        public void Perimeter()
        {
            int n = 10;
            mPerimeter = 2 * n * mRadius * (float)Math.Sin(Math.PI / n);
        }

        // Área del decágono regular: A = (n / 2) * R² * sin(2π / n)
        public void Area()
        {
            int n = 10;
            mArea = (n / 2.0f) * mRadius * mRadius * (float)Math.Sin((2 * Math.PI) / n);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtRadius, TextBox txtPerimeter, TextBox txtArea)
        {
            mRadius = 0.0f;
            mPerimeter = 0.0f;
            mArea = 0.0f;

            txtRadius.Clear();
            txtPerimeter.Clear();
            txtArea.Clear();

            txtRadius.Focus();
        }

        public void CloseForm(Form form)
        {
            form.Close();
        }
    }
}
