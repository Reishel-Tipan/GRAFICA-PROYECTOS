namespace Graficar_lineas.Views
{
    partial class FrmCurvaBézier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCurvaBézier));
            this.txtNumerodePuntos = new System.Windows.Forms.TextBox();
            this.BtnBezier = new System.Windows.Forms.Button();
            this.BtnStartAnimation = new System.Windows.Forms.Button();
            this.pictuCanva = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiarTodo = new System.Windows.Forms.Button();
            this.ColorBox = new System.Windows.Forms.GroupBox();
            this.pic_color = new System.Windows.Forms.Button();
            this.BtnColorSet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInicio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictuCanva)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.ColorBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNumerodePuntos
            // 
            this.txtNumerodePuntos.Location = new System.Drawing.Point(48, 333);
            this.txtNumerodePuntos.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumerodePuntos.Name = "txtNumerodePuntos";
            this.txtNumerodePuntos.Size = new System.Drawing.Size(98, 22);
            this.txtNumerodePuntos.TabIndex = 66;
            // 
            // BtnBezier
            // 
            this.BtnBezier.BackColor = System.Drawing.SystemColors.Window;
            this.BtnBezier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnBezier.Location = new System.Drawing.Point(170, 326);
            this.BtnBezier.Margin = new System.Windows.Forms.Padding(4);
            this.BtnBezier.Name = "BtnBezier";
            this.BtnBezier.Size = new System.Drawing.Size(97, 37);
            this.BtnBezier.TabIndex = 65;
            this.BtnBezier.Text = "Empezar";
            this.BtnBezier.UseVisualStyleBackColor = false;
            // 
            // BtnStartAnimation
            // 
            this.BtnStartAnimation.BackColor = System.Drawing.SystemColors.Window;
            this.BtnStartAnimation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnStartAnimation.Location = new System.Drawing.Point(170, 387);
            this.BtnStartAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStartAnimation.Name = "BtnStartAnimation";
            this.BtnStartAnimation.Size = new System.Drawing.Size(97, 37);
            this.BtnStartAnimation.TabIndex = 58;
            this.BtnStartAnimation.Text = "Animar";
            this.BtnStartAnimation.UseVisualStyleBackColor = false;
            // 
            // pictuCanva
            // 
            this.pictuCanva.BackColor = System.Drawing.SystemColors.Window;
            this.pictuCanva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictuCanva.Location = new System.Drawing.Point(8, 23);
            this.pictuCanva.Margin = new System.Windows.Forms.Padding(5);
            this.pictuCanva.Name = "pictuCanva";
            this.pictuCanva.Size = new System.Drawing.Size(679, 492);
            this.pictuCanva.TabIndex = 57;
            this.pictuCanva.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiarTodo);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(279, 119);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Herramienta";
            // 
            // btnLimpiarTodo
            // 
            this.btnLimpiarTodo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiarTodo.BackgroundImage")));
            this.btnLimpiarTodo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpiarTodo.Location = new System.Drawing.Point(85, 36);
            this.btnLimpiarTodo.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiarTodo.Name = "btnLimpiarTodo";
            this.btnLimpiarTodo.Size = new System.Drawing.Size(68, 53);
            this.btnLimpiarTodo.TabIndex = 16;
            this.btnLimpiarTodo.UseVisualStyleBackColor = true;
            // 
            // ColorBox
            // 
            this.ColorBox.Controls.Add(this.pic_color);
            this.ColorBox.Controls.Add(this.BtnColorSet);
            this.ColorBox.Location = new System.Drawing.Point(12, 136);
            this.ColorBox.Margin = new System.Windows.Forms.Padding(4);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Padding = new System.Windows.Forms.Padding(4);
            this.ColorBox.Size = new System.Drawing.Size(279, 108);
            this.ColorBox.TabIndex = 59;
            this.ColorBox.TabStop = false;
            this.ColorBox.Text = "Colores";
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.Black;
            this.pic_color.Location = new System.Drawing.Point(59, 37);
            this.pic_color.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(53, 39);
            this.pic_color.TabIndex = 32;
            this.pic_color.UseVisualStyleBackColor = false;
            // 
            // BtnColorSet
            // 
            this.BtnColorSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnColorSet.BackgroundImage")));
            this.BtnColorSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnColorSet.Location = new System.Drawing.Point(143, 29);
            this.BtnColorSet.Margin = new System.Windows.Forms.Padding(4);
            this.BtnColorSet.Name = "BtnColorSet";
            this.BtnColorSet.Size = new System.Drawing.Size(61, 55);
            this.BtnColorSet.TabIndex = 10;
            this.BtnColorSet.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictuCanva);
            this.groupBox2.Location = new System.Drawing.Point(336, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(697, 523);
            this.groupBox2.TabIndex = 67;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dibuja aquí!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 68;
            this.label1.Text = "Ingresa el número de puntos:";
            // 
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Location = new System.Drawing.Point(186, 446);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(62, 46);
            this.btnInicio.TabIndex = 69;
            this.btnInicio.Text = "🏠";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // FrmCurvaBézier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1045, 546);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtNumerodePuntos);
            this.Controls.Add(this.BtnBezier);
            this.Controls.Add(this.BtnStartAnimation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ColorBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCurvaBézier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Curvas de Bézier";
            this.Load += new System.EventHandler(this.FrmCurvaBézier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictuCanva)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ColorBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumerodePuntos;
        private System.Windows.Forms.Button BtnBezier;
        private System.Windows.Forms.Button BtnStartAnimation;
        private System.Windows.Forms.PictureBox pictuCanva;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpiarTodo;
        private System.Windows.Forms.GroupBox ColorBox;
        private System.Windows.Forms.Button pic_color;
        private System.Windows.Forms.Button BtnColorSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInicio;
    }
}