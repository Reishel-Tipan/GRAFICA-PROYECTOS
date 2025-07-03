namespace Graficar_lineas.Views
{
    partial class FrmScanline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScanline));
            this.BtnColorSet = new System.Windows.Forms.Button();
            this.pic_color = new System.Windows.Forms.Button();
            this.ColorBox = new System.Windows.Forms.GroupBox();
            this.rellenar = new System.Windows.Forms.Button();
            this.BtnLine = new System.Windows.Forms.Button();
            this.btnLimpiarTodo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictuCanva = new System.Windows.Forms.PictureBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnInicio = new System.Windows.Forms.Button();
            this.ColorBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictuCanva)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnColorSet
            // 
            this.BtnColorSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnColorSet.BackgroundImage")));
            this.BtnColorSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnColorSet.Location = new System.Drawing.Point(132, 19);
            this.BtnColorSet.Margin = new System.Windows.Forms.Padding(4);
            this.BtnColorSet.Name = "BtnColorSet";
            this.BtnColorSet.Size = new System.Drawing.Size(61, 55);
            this.BtnColorSet.TabIndex = 10;
            this.BtnColorSet.UseVisualStyleBackColor = true;
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.Black;
            this.pic_color.Location = new System.Drawing.Point(58, 27);
            this.pic_color.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(53, 39);
            this.pic_color.TabIndex = 32;
            this.pic_color.UseVisualStyleBackColor = false;
            // 
            // ColorBox
            // 
            this.ColorBox.Controls.Add(this.pic_color);
            this.ColorBox.Controls.Add(this.BtnColorSet);
            this.ColorBox.Location = new System.Drawing.Point(12, 101);
            this.ColorBox.Margin = new System.Windows.Forms.Padding(4);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Padding = new System.Windows.Forms.Padding(4);
            this.ColorBox.Size = new System.Drawing.Size(266, 76);
            this.ColorBox.TabIndex = 55;
            this.ColorBox.TabStop = false;
            this.ColorBox.Text = "Colores";
            // 
            // rellenar
            // 
            this.rellenar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rellenar.BackgroundImage")));
            this.rellenar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rellenar.Location = new System.Drawing.Point(104, 21);
            this.rellenar.Margin = new System.Windows.Forms.Padding(4);
            this.rellenar.Name = "rellenar";
            this.rellenar.Size = new System.Drawing.Size(48, 48);
            this.rellenar.TabIndex = 4;
            this.rellenar.UseVisualStyleBackColor = true;
            // 
            // BtnLine
            // 
            this.BtnLine.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLine.BackgroundImage")));
            this.BtnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnLine.Location = new System.Drawing.Point(23, 21);
            this.BtnLine.Margin = new System.Windows.Forms.Padding(4);
            this.BtnLine.Name = "BtnLine";
            this.BtnLine.Size = new System.Drawing.Size(48, 48);
            this.BtnLine.TabIndex = 0;
            this.BtnLine.UseVisualStyleBackColor = false;
            // 
            // btnLimpiarTodo
            // 
            this.btnLimpiarTodo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiarTodo.BackgroundImage")));
            this.btnLimpiarTodo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpiarTodo.Location = new System.Drawing.Point(170, 21);
            this.btnLimpiarTodo.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiarTodo.Name = "btnLimpiarTodo";
            this.btnLimpiarTodo.Size = new System.Drawing.Size(54, 48);
            this.btnLimpiarTodo.TabIndex = 16;
            this.btnLimpiarTodo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiarTodo);
            this.groupBox1.Controls.Add(this.BtnLine);
            this.groupBox1.Controls.Add(this.rellenar);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(266, 84);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Herramientas";
            // 
            // pictuCanva
            // 
            this.pictuCanva.BackColor = System.Drawing.SystemColors.Control;
            this.pictuCanva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictuCanva.Location = new System.Drawing.Point(8, 23);
            this.pictuCanva.Margin = new System.Windows.Forms.Padding(5);
            this.pictuCanva.Name = "pictuCanva";
            this.pictuCanva.Size = new System.Drawing.Size(664, 494);
            this.pictuCanva.TabIndex = 54;
            this.pictuCanva.TabStop = false;
            this.pictuCanva.Paint += new System.Windows.Forms.PaintEventHandler(this.PictuCanva_Paint);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(7, 22);
            this.listBox.Margin = new System.Windows.Forms.Padding(4);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(252, 324);
            this.listBox.TabIndex = 59;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictuCanva);
            this.groupBox2.Location = new System.Drawing.Point(285, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 525);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gráfico";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 352);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Píxeles pintados";
            // 
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Location = new System.Drawing.Point(971, 482);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(62, 46);
            this.btnInicio.TabIndex = 64;
            this.btnInicio.Text = "🏠";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // FrmScanline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 546);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ColorBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmScanline";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algoritmo de Relleno Scanline";
            this.Load += new System.EventHandler(this.FrmScanline_Load);
            this.ColorBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictuCanva)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnColorSet;
        private System.Windows.Forms.Button pic_color;
        private System.Windows.Forms.GroupBox ColorBox;
        private System.Windows.Forms.Button rellenar;
        private System.Windows.Forms.Button BtnLine;
        private System.Windows.Forms.Button btnLimpiarTodo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictuCanva;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnInicio;
    }
}