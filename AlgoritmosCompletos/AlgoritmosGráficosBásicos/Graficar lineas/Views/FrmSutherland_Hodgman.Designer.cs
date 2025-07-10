namespace Graficar_lineas.Views
{
    partial class FrmSutherland_Hodgman
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gbControles = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRecortar = new System.Windows.Forms.Button();
            this.pbxLienzo = new System.Windows.Forms.PictureBox();
            this.btnInicio = new System.Windows.Forms.Button();
            this.gbControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 524);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1045, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gbControles
            // 
            this.gbControles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControles.Controls.Add(this.btnInicio);
            this.gbControles.Controls.Add(this.btnLimpiar);
            this.gbControles.Controls.Add(this.btnRecortar);
            this.gbControles.Location = new System.Drawing.Point(32, 12);
            this.gbControles.Name = "gbControles";
            this.gbControles.Size = new System.Drawing.Size(200, 301);
            this.gbControles.TabIndex = 7;
            this.gbControles.TabStop = false;
            this.gbControles.Text = "Controles";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.Window;
            this.btnLimpiar.Location = new System.Drawing.Point(42, 112);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(119, 51);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnRecortar
            // 
            this.btnRecortar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecortar.BackColor = System.Drawing.SystemColors.Window;
            this.btnRecortar.Enabled = false;
            this.btnRecortar.Location = new System.Drawing.Point(42, 41);
            this.btnRecortar.Name = "btnRecortar";
            this.btnRecortar.Size = new System.Drawing.Size(119, 51);
            this.btnRecortar.TabIndex = 0;
            this.btnRecortar.Text = "Recortar";
            this.btnRecortar.UseVisualStyleBackColor = false;
            // 
            // pbxLienzo
            // 
            this.pbxLienzo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLienzo.BackColor = System.Drawing.Color.White;
            this.pbxLienzo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxLienzo.Location = new System.Drawing.Point(273, 12);
            this.pbxLienzo.Name = "pbxLienzo";
            this.pbxLienzo.Size = new System.Drawing.Size(736, 498);
            this.pbxLienzo.TabIndex = 6;
            this.pbxLienzo.TabStop = false;
            // 
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Location = new System.Drawing.Point(64, 198);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(62, 46);
            this.btnInicio.TabIndex = 13;
            this.btnInicio.Text = "🏠";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // FrmSutherland_Hodgman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 546);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbControles);
            this.Controls.Add(this.pbxLienzo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSutherland_Hodgman";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recorte Sutherland_Hodgman";
            this.gbControles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox gbControles;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRecortar;
        private System.Windows.Forms.PictureBox pbxLienzo;
        private System.Windows.Forms.Button btnInicio;
    }
}