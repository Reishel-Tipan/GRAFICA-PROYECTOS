using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graficar_lineas.Views;

namespace Graficar_lineas
{
    public partial class FrmHome : Form
    {

        public FrmHome()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Esto habilita el contenedor MDI
        }
        private void CerrarFormulariosHijos()
        {
            // Recorrer todos los formularios abiertos (hijos)
            foreach (Form form in this.MdiChildren)
            {
                // Cerrar todos los formularios hijos
                form.Close();
            }
        }

        private void HideLabel()
        {
            lblTitulo.Visible = false;
            label1.Visible = false;
        }


        private void FrmInicio_Load(object sender, EventArgs e)
        {

        }

        private void algoritmoDDAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            // Verificar si FrmBresenham ya está abierto, si es así, lo activamos
            FrmDDA DDAForm = new FrmDDA();
            DDAForm.MdiParent = this; // Establecer este formulario como MDI Parent
            DDAForm.Show();
        }

        private void algoritmoDeBresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            // Verificar si FrmBresenham ya está abierto, si es así, lo activamos
            FrmBresenham BresenhamForm = new FrmBresenham();
            BresenhamForm.MdiParent = this; // Establecer este formulario como MDI Parent
            BresenhamForm.Show();
        }

        private void disToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            // Verificar si FrmBresenham ya está abierto, si es así, lo activamos
            FrmCircunferencia CircunferenciaForm = new FrmCircunferencia();
            CircunferenciaForm.MdiParent = this; // Establecer este formulario como MDI Parent
            CircunferenciaForm.Show();
        }

        private void rellenadoDeInundaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            // Verificar si FrmBresenham ya está abierto, si es así, lo activamos
            FrmRellenado RellenadoForm = new FrmRellenado();
            RellenadoForm.MdiParent = this; // Establecer este formulario como MDI Parent
            RellenadoForm.Show();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void bresenhamParaElipsesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmElipseBresenham ElipseForm = new FrmElipseBresenham();
            ElipseForm.MdiParent = this; // Establecer este formulario como MDI Parent
            ElipseForm.Show();
        }

        private void scanlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmScanline ScalineForm = new FrmScanline();
            ScalineForm.MdiParent = this; // Establecer este formulario como MDI Parent
            ScalineForm.Show();
        }

        private void curvasDeBézierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmCurvaBézier BézierForm = new FrmCurvaBézier();
            BézierForm.MdiParent = this; // Establecer este formulario como MDI Parent
            BézierForm.Show();

        }

        private void curvasBsplinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmCurvaB_Splines splineForm = new FrmCurvaB_Splines();
            splineForm.MdiParent = this; // Establecer este formulario como MDI Parent
            splineForm.Show();
        }

        private void cohenSutherlandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmCohen_Sutherland cohenForm = new FrmCohen_Sutherland();
            cohenForm.MdiParent = this; // Establecer este formulario como MDI Parent
            cohenForm.Show();
        }

        private void sutherlandHodgmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideLabel();
            CerrarFormulariosHijos();
            FrmSutherland_Hodgman sutherlandForm = new FrmSutherland_Hodgman();
            sutherlandForm.MdiParent = this; // Establecer este formulario como MDI Parent
            sutherlandForm.Show();

        }
    }
    
}
