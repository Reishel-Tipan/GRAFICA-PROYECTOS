namespace Graficar_lineas
{
    partial class FrmDDA
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picDibujo = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGraficar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labely2 = new System.Windows.Forms.Label();
            this.labelx2 = new System.Windows.Forms.Label();
            this.labelY1 = new System.Windows.Forms.Label();
            this.labelx1 = new System.Windows.Forms.Label();
            this.txtY_2 = new System.Windows.Forms.TextBox();
            this.txtX_2 = new System.Windows.Forms.TextBox();
            this.txtY_1 = new System.Windows.Forms.TextBox();
            this.txtX_1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxPuntos = new System.Windows.Forms.ListBox();
            this.btnReseat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDibujo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picDibujo);
            this.groupBox1.Location = new System.Drawing.Point(372, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(660, 520);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Píxeles";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // picDibujo
            // 
            this.picDibujo.Location = new System.Drawing.Point(8, 23);
            this.picDibujo.Margin = new System.Windows.Forms.Padding(4);
            this.picDibujo.Name = "picDibujo";
            this.picDibujo.Size = new System.Drawing.Size(644, 489);
            this.picDibujo.TabIndex = 0;
            this.picDibujo.TabStop = false;
            this.picDibujo.Click += new System.EventHandler(this.picDibujo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReseat);
            this.groupBox2.Controls.Add(this.btnGraficar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labely2);
            this.groupBox2.Controls.Add(this.labelx2);
            this.groupBox2.Controls.Add(this.labelY1);
            this.groupBox2.Controls.Add(this.labelx1);
            this.groupBox2.Controls.Add(this.txtY_2);
            this.groupBox2.Controls.Add(this.txtX_2);
            this.groupBox2.Controls.Add(this.txtY_1);
            this.groupBox2.Controls.Add(this.txtX_1);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(351, 212);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos";
            // 
            // btnGraficar
            // 
            this.btnGraficar.Location = new System.Drawing.Point(203, 56);
            this.btnGraficar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGraficar.Name = "btnGraficar";
            this.btnGraficar.Size = new System.Drawing.Size(97, 37);
            this.btnGraficar.TabIndex = 10;
            this.btnGraficar.Text = "Graficar";
            this.btnGraficar.UseVisualStyleBackColor = true;
            this.btnGraficar.Click += new System.EventHandler(this.BtnGraficar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Punto 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Punto1";
            // 
            // labely2
            // 
            this.labely2.AutoSize = true;
            this.labely2.Location = new System.Drawing.Point(11, 174);
            this.labely2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labely2.Name = "labely2";
            this.labely2.Size = new System.Drawing.Size(67, 16);
            this.labely2.TabIndex = 7;
            this.labely2.Text = "Ingrese Y:";
            // 
            // labelx2
            // 
            this.labelx2.AutoSize = true;
            this.labelx2.Location = new System.Drawing.Point(11, 144);
            this.labelx2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelx2.Name = "labelx2";
            this.labelx2.Size = new System.Drawing.Size(66, 16);
            this.labelx2.TabIndex = 6;
            this.labelx2.Text = "Ingrese X:";
            // 
            // labelY1
            // 
            this.labelY1.AutoSize = true;
            this.labelY1.Location = new System.Drawing.Point(11, 77);
            this.labelY1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelY1.Name = "labelY1";
            this.labelY1.Size = new System.Drawing.Size(67, 16);
            this.labelY1.TabIndex = 5;
            this.labelY1.Text = "Ingrese Y:";
            this.labelY1.Click += new System.EventHandler(this.labelY1_Click);
            // 
            // labelx1
            // 
            this.labelx1.AutoSize = true;
            this.labelx1.Location = new System.Drawing.Point(12, 42);
            this.labelx1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelx1.Name = "labelx1";
            this.labelx1.Size = new System.Drawing.Size(66, 16);
            this.labelx1.TabIndex = 4;
            this.labelx1.Text = "Ingrese X:";
            this.labelx1.Click += new System.EventHandler(this.labelx1_Click);
            // 
            // txtY_2
            // 
            this.txtY_2.Location = new System.Drawing.Point(84, 174);
            this.txtY_2.Margin = new System.Windows.Forms.Padding(4);
            this.txtY_2.Name = "txtY_2";
            this.txtY_2.Size = new System.Drawing.Size(52, 22);
            this.txtY_2.TabIndex = 3;
            this.txtY_2.TextChanged += new System.EventHandler(this.txtY_2_TextChanged);
            // 
            // txtX_2
            // 
            this.txtX_2.Location = new System.Drawing.Point(84, 141);
            this.txtX_2.Margin = new System.Windows.Forms.Padding(4);
            this.txtX_2.Name = "txtX_2";
            this.txtX_2.Size = new System.Drawing.Size(52, 22);
            this.txtX_2.TabIndex = 2;
            this.txtX_2.TextChanged += new System.EventHandler(this.txtX_2_TextChanged);
            // 
            // txtY_1
            // 
            this.txtY_1.Location = new System.Drawing.Point(84, 74);
            this.txtY_1.Margin = new System.Windows.Forms.Padding(4);
            this.txtY_1.Name = "txtY_1";
            this.txtY_1.Size = new System.Drawing.Size(52, 22);
            this.txtY_1.TabIndex = 1;
            this.txtY_1.TextChanged += new System.EventHandler(this.txtY_1_TextChanged);
            // 
            // txtX_1
            // 
            this.txtX_1.Location = new System.Drawing.Point(84, 39);
            this.txtX_1.Margin = new System.Windows.Forms.Padding(4);
            this.txtX_1.Name = "txtX_1";
            this.txtX_1.Size = new System.Drawing.Size(52, 22);
            this.txtX_1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxPuntos);
            this.groupBox3.Location = new System.Drawing.Point(13, 233);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(351, 300);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Píxeles encendidos";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // listBoxPuntos
            // 
            this.listBoxPuntos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPuntos.FormattingEnabled = true;
            this.listBoxPuntos.ItemHeight = 16;
            this.listBoxPuntos.Location = new System.Drawing.Point(4, 19);
            this.listBoxPuntos.Name = "listBoxPuntos";
            this.listBoxPuntos.Size = new System.Drawing.Size(343, 277);
            this.listBoxPuntos.TabIndex = 3;
            // 
            // btnReseat
            // 
            this.btnReseat.Location = new System.Drawing.Point(203, 111);
            this.btnReseat.Margin = new System.Windows.Forms.Padding(4);
            this.btnReseat.Name = "btnReseat";
            this.btnReseat.Size = new System.Drawing.Size(97, 37);
            this.btnReseat.TabIndex = 11;
            this.btnReseat.Text = "Limpiar";
            this.btnReseat.UseVisualStyleBackColor = true;
            this.btnReseat.Click += new System.EventHandler(this.BtnReseat_Click);
            // 
            // FrmDDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1045, 546);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algoritmo DDA";
            this.Load += new System.EventHandler(this.FrmLineas_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDibujo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picDibujo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtY_2;
        private System.Windows.Forms.TextBox txtX_2;
        private System.Windows.Forms.TextBox txtY_1;
        private System.Windows.Forms.TextBox txtX_1;
        private System.Windows.Forms.Label labely2;
        private System.Windows.Forms.Label labelx2;
        private System.Windows.Forms.Label labelY1;
        private System.Windows.Forms.Label labelx1;
        private System.Windows.Forms.Button btnGraficar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxPuntos;
        private System.Windows.Forms.Button btnReseat;
    }
}

