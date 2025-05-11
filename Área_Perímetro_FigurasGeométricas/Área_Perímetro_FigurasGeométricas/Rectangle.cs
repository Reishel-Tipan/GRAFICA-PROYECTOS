using System;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    internal class Rectangle
    {
        private float baseRec, height, area, perimeter;

        public void ReadData(TextBox txtBase, TextBox txtHeight)
        {
            string inputBase = txtBase.Text.Trim();
            string inputHeight = txtHeight.Text.Trim();

            if (!float.TryParse(inputBase, out baseRec) || baseRec <= 0 ||
                !float.TryParse(inputHeight, out height) || height <= 0)
            {
                MessageBox.Show("Ingrese valores válidos, positivos y mayores a cero para base y altura.", "Error");
                baseRec = height = 0;
            }
        }

        public void CalculateArea()
        {
            area = baseRec * height;
        }

        public void CalculatePerimeter()
        {
            perimeter = 2 * (baseRec + height);
        }

        public void PrintData(TextBox txtPerimeter, TextBox txtArea)
        {
            txtPerimeter.Text = perimeter.ToString("F2");
            txtArea.Text = area.ToString("F2");
        }

        public void InitializeData(TextBox txtBase, TextBox txtHeight, TextBox txtPerimeter, TextBox txtArea)
        {
            baseRec = height = area = perimeter = 0;
            txtBase.Text = "";
            txtHeight.Text = "";
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
