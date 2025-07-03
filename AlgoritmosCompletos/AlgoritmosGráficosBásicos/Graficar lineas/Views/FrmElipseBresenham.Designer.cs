namespace Graficar_lineas.Views
{
    partial class FrmElipseBresenham
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxPuntos = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGraficar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labely2 = new System.Windows.Forms.Label();
            this.labelx2 = new System.Windows.Forms.Label();
            this.labelY1 = new System.Windows.Forms.Label();
            this.labelx1 = new System.Windows.Forms.Label();
            this.txtRadioMenor = new System.Windows.Forms.TextBox();
            this.txtRadioMayor = new System.Windows.Forms.TextBox();
            this.txtCentroY = new System.Windows.Forms.TextBox();
            this.txtCentroX = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxPuntos);
            this.groupBox3.Location = new System.Drawing.Point(12, 231);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(351, 300);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Píxeles encendidos";
            // 
            // listBoxPuntos
            // 
            this.listBoxPuntos.FormattingEnabled = true;
            this.listBoxPuntos.ItemHeight = 16;
            this.listBoxPuntos.Location = new System.Drawing.Point(4, 19);
            this.listBoxPuntos.Name = "listBoxPuntos";
            this.listBoxPuntos.Size = new System.Drawing.Size(343, 276);
            this.listBoxPuntos.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.btnLimpiar);
            this.groupBox2.Controls.Add(this.btnGraficar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labely2);
            this.groupBox2.Controls.Add(this.labelx2);
            this.groupBox2.Controls.Add(this.labelY1);
            this.groupBox2.Controls.Add(this.labelx1);
            this.groupBox2.Controls.Add(this.txtRadioMenor);
            this.groupBox2.Controls.Add(this.txtRadioMayor);
            this.groupBox2.Controls.Add(this.txtCentroY);
            this.groupBox2.Controls.Add(this.txtCentroX);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(352, 212);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos";
            // 
            // btnGraficar
            // 
            this.btnGraficar.Location = new System.Drawing.Point(203, 56);
            this.btnGraficar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGraficar.Name = "btnGraficar";
            this.btnGraficar.Size = new System.Drawing.Size(97, 37);
            this.btnGraficar.TabIndex = 10;
            this.btnGraficar.Text = "Graficar";
            this.btnGraficar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Radio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Centro";
            // 
            // labely2
            // 
            this.labely2.AutoSize = true;
            this.labely2.Location = new System.Drawing.Point(11, 174);
            this.labely2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labely2.Name = "labely2";
            this.labely2.Size = new System.Drawing.Size(88, 16);
            this.labely2.TabIndex = 7;
            this.labely2.Text = "Radio Menor:";
            // 
            // labelx2
            // 
            this.labelx2.AutoSize = true;
            this.labelx2.Location = new System.Drawing.Point(11, 144);
            this.labelx2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelx2.Name = "labelx2";
            this.labelx2.Size = new System.Drawing.Size(88, 16);
            this.labelx2.TabIndex = 6;
            this.labelx2.Text = "Radio Mayor:";
            // 
            // labelY1
            // 
            this.labelY1.AutoSize = true;
            this.labelY1.Location = new System.Drawing.Point(32, 77);
            this.labelY1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelY1.Name = "labelY1";
            this.labelY1.Size = new System.Drawing.Size(61, 16);
            this.labelY1.TabIndex = 5;
            this.labelY1.Text = "Centro Y:";
            // 
            // labelx1
            // 
            this.labelx1.AutoSize = true;
            this.labelx1.Location = new System.Drawing.Point(33, 45);
            this.labelx1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelx1.Name = "labelx1";
            this.labelx1.Size = new System.Drawing.Size(60, 16);
            this.labelx1.TabIndex = 4;
            this.labelx1.Text = "Centro X:";
            // 
            // txtRadioMenor
            // 
            this.txtRadioMenor.Location = new System.Drawing.Point(107, 174);
            this.txtRadioMenor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRadioMenor.Name = "txtRadioMenor";
            this.txtRadioMenor.Size = new System.Drawing.Size(52, 22);
            this.txtRadioMenor.TabIndex = 3;
            // 
            // txtRadioMayor
            // 
            this.txtRadioMayor.Location = new System.Drawing.Point(107, 141);
            this.txtRadioMayor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRadioMayor.Name = "txtRadioMayor";
            this.txtRadioMayor.Size = new System.Drawing.Size(52, 22);
            this.txtRadioMayor.TabIndex = 2;
            // 
            // txtCentroY
            // 
            this.txtCentroY.Location = new System.Drawing.Point(107, 77);
            this.txtCentroY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCentroY.Name = "txtCentroY";
            this.txtCentroY.Size = new System.Drawing.Size(52, 22);
            this.txtCentroY.TabIndex = 1;
            // 
            // txtCentroX
            // 
            this.txtCentroX.Location = new System.Drawing.Point(107, 45);
            this.txtCentroX.Name = "txtCentroX";
            this.txtCentroX.Size = new System.Drawing.Size(52, 22);
            this.txtCentroX.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picCanvas);
            this.groupBox1.Location = new System.Drawing.Point(369, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 520);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Píxeles";
            // 
            // picCanvas
            // 
            this.picCanvas.Location = new System.Drawing.Point(6, 21);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(644, 489);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Click += new System.EventHandler(this.picCanvas_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(203, 111);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(97, 37);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // FrmElipseBresenham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1045, 546);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmElipseBresenham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bresenham para Elipses";
            this.Load += new System.EventHandler(this.FrmElipseBresenham_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxPuntos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGraficar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labely2;
        private System.Windows.Forms.Label labelx2;
        private System.Windows.Forms.Label labelY1;
        private System.Windows.Forms.Label labelx1;
        private System.Windows.Forms.TextBox txtRadioMenor;
        private System.Windows.Forms.TextBox txtRadioMayor;
        private System.Windows.Forms.TextBox txtCentroY;
        private System.Windows.Forms.TextBox txtCentroX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnLimpiar;
    }
}