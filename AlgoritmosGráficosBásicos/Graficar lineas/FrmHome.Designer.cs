namespace Graficar_lineas
{
    partial class FrmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.algoritmoDDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDDAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDeBresenhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rellenadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rellenadoDeInundaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoritmoDDAToolStripMenuItem,
            this.algoritmoToolStripMenuItem,
            this.rellenadoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1204, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // algoritmoDDAToolStripMenuItem
            // 
            this.algoritmoDDAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoritmoDDAToolStripMenuItem1,
            this.algoritmoDeBresenhamToolStripMenuItem});
            this.algoritmoDDAToolStripMenuItem.Name = "algoritmoDDAToolStripMenuItem";
            this.algoritmoDDAToolStripMenuItem.Size = new System.Drawing.Size(64, 26);
            this.algoritmoDDAToolStripMenuItem.Text = "Líneas";
            // 
            // algoritmoDDAToolStripMenuItem1
            // 
            this.algoritmoDDAToolStripMenuItem1.Name = "algoritmoDDAToolStripMenuItem1";
            this.algoritmoDDAToolStripMenuItem1.Size = new System.Drawing.Size(258, 26);
            this.algoritmoDDAToolStripMenuItem1.Text = "Algoritmo DDA";
            this.algoritmoDDAToolStripMenuItem1.Click += new System.EventHandler(this.algoritmoDDAToolStripMenuItem1_Click);
            // 
            // algoritmoDeBresenhamToolStripMenuItem
            // 
            this.algoritmoDeBresenhamToolStripMenuItem.Name = "algoritmoDeBresenhamToolStripMenuItem";
            this.algoritmoDeBresenhamToolStripMenuItem.Size = new System.Drawing.Size(258, 26);
            this.algoritmoDeBresenhamToolStripMenuItem.Text = "Algoritmo de Bresenham";
            this.algoritmoDeBresenhamToolStripMenuItem.Click += new System.EventHandler(this.algoritmoDeBresenhamToolStripMenuItem_Click);
            // 
            // algoritmoToolStripMenuItem
            // 
            this.algoritmoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disToolStripMenuItem});
            this.algoritmoToolStripMenuItem.Name = "algoritmoToolStripMenuItem";
            this.algoritmoToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.algoritmoToolStripMenuItem.Text = "Circunferencias";
            // 
            // disToolStripMenuItem
            // 
            this.disToolStripMenuItem.Name = "disToolStripMenuItem";
            this.disToolStripMenuItem.Size = new System.Drawing.Size(263, 26);
            this.disToolStripMenuItem.Text = "Bresenham Circunferencia";
            this.disToolStripMenuItem.Click += new System.EventHandler(this.disToolStripMenuItem_Click);
            // 
            // rellenadoToolStripMenuItem
            // 
            this.rellenadoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rellenadoDeInundaciónToolStripMenuItem});
            this.rellenadoToolStripMenuItem.Name = "rellenadoToolStripMenuItem";
            this.rellenadoToolStripMenuItem.Size = new System.Drawing.Size(90, 26);
            this.rellenadoToolStripMenuItem.Text = "Rellenado";
            // 
            // rellenadoDeInundaciónToolStripMenuItem
            // 
            this.rellenadoDeInundaciónToolStripMenuItem.Name = "rellenadoDeInundaciónToolStripMenuItem";
            this.rellenadoDeInundaciónToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.rellenadoDeInundaciónToolStripMenuItem.Text = "Rellenado de inundación";
            this.rellenadoDeInundaciónToolStripMenuItem.Click += new System.EventHandler(this.rellenadoDeInundaciónToolStripMenuItem_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(274, 267);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(636, 54);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Algoritmos Gráficos Básicos";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(429, 367);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 54);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reishel Tipán";
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1204, 696);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoToolStripMenuItem;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDDAToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDeBresenhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rellenadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rellenadoDeInundaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}