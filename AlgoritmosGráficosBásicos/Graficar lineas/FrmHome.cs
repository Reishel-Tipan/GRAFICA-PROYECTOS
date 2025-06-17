using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public partial class FrmHome : Form
    {
        private FrmBresenham BresenhamForm;
        private FrmLineas LineasForm;

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
            FrmLineas DDAForm = new FrmLineas();
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
    }
}
