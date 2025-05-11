using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Hexagon
    {
        private float mRadius;
        private float mPerimeter;
        private float mArea;

        public Hexagon()
        {
            mRadius = 0.0f;
            mPerimeter = 0.0f;
            mArea = 0.0f;
        }

        public void ReadData(TextBox txtRadius)
        {
            string input = txtRadius.Text.Trim();
            if (float.TryParse(input, out mRadius) && mRadius > 0)
            {
                return;
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número positivo válido para el radio.", "Error de entrada");
                mRadius = 0;
            }
        }

        public void CalculatePerimeter()
        {
            float side = 2 * mRadius * (float)Math.Sin(Math.PI / 6); // 30 grados
            mPerimeter = 6 * side;
        }

        public void CalculateArea()
        {
            mArea = (6.0f / 2.0f) * mRadius * mRadius * (float)Math.Sin(2 * Math.PI / 6);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = mPerimeter.ToString("F2");
            txtArea.Text = mArea.ToString("F2");
        }

        public void InitializeData(TextBox txtRadius, TextBox txtPerimeter, TextBox txtArea)
        {
            mRadius = 0.0f;
            mPerimeter = 0.0f;
            mArea = 0.0f;
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
