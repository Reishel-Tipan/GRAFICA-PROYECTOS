using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Área_Perímetro_FigurasGeométricas
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void triánguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTriangle triangle = new FrmTriangle();
            triangle.MdiParent = this;
            triangle.Show();
        }

        private void círculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCircle circle = new FrmCircle();
            circle.MdiParent = this;
            circle.Show();
        }

        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEllipse ellipse = new FrmEllipse();
            ellipse.MdiParent = this;
            ellipse.Show();
        }

        private void óvaloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOval oval = new FrmOval();
            oval.MdiParent = this;
            oval.Show();
        }

        private void cuadradoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSquare square = new FrmSquare();
            square.MdiParent = this;
            square.Show();
        }

        private void rectánguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRectangle rectangle = new FrmRectangle();
            rectangle.MdiParent = this;
            rectangle.Show();
        }

        private void trapecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTrapezoid trapezoid = new FrmTrapezoid();
            trapezoid.MdiParent = this;
            trapezoid.Show();
        }

        private void trapezoideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTrapezium trapezium = new FrmTrapezium();
            trapezium.MdiParent = this;
            trapezium.Show();
        }

        private void trapecioIsóscelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIsoscelesTrapezoid isoscelesTrapezoid = new FrmIsoscelesTrapezoid();
            isoscelesTrapezoid.MdiParent = this;
            isoscelesTrapezoid.Show();
        }

        private void deltoideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKite kite = new FrmKite();
            kite.MdiParent = this;
            kite.Show();
        }

        private void romboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRhombus rhombus = new FrmRhombus();
            rhombus.MdiParent = this;
            rhombus.Show();
        }

        private void romboideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRhomboid rhomboid = new FrmRhomboid();
            rhomboid.MdiParent = this;
            rhomboid.Show();
        }

        private void pentágonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPentagon pentagono = new FrmPentagon();
            pentagono.MdiParent = this;
            pentagono.Show();
        }

        private void hexágonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHexagon hexagon = new FrmHexagon();
            hexagon.MdiParent = this;
            hexagon.Show();
        }

        private void heptágonoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmHeptagon heptagon = new FrmHeptagon();
            heptagon.MdiParent = this;
            heptagon.Show();
        }

        private void octágonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOctagon octagon = new FrmOctagon();
            octagon.MdiParent = this;
            octagon.Show();
        }

        private void decágonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDecagon decagon = new FrmDecagon();
            decagon.MdiParent = this;
            decagon.Show();
        }

        private void eneánogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNonagon nonagon = new FrmNonagon();
            nonagon.MdiParent = this;
            nonagon.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
