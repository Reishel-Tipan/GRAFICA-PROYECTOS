namespace Área_Perímetro_FigurasGeométricas
{
    partial class FrmTrapezoid
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
            this.grbCanva = new System.Windows.Forms.GroupBox();
            this.grbOutputs = new System.Windows.Forms.GroupBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtPerimeter = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblPerimeter = new System.Windows.Forms.Label();
            this.grbProcess = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.grbInputs = new System.Windows.Forms.GroupBox();
            this.txtSide3 = new System.Windows.Forms.TextBox();
            this.txtSide2 = new System.Windows.Forms.TextBox();
            this.txtSide1 = new System.Windows.Forms.TextBox();
            this.txtSide4 = new System.Windows.Forms.TextBox();
            this.lblSide3 = new System.Windows.Forms.Label();
            this.lblSide2 = new System.Windows.Forms.Label();
            this.lblSide1 = new System.Windows.Forms.Label();
            this.lblSide4 = new System.Windows.Forms.Label();
            this.grbOutputs.SuspendLayout();
            this.grbProcess.SuspendLayout();
            this.grbInputs.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbCanva
            // 
            this.grbCanva.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grbCanva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbCanva.Location = new System.Drawing.Point(371, 12);
            this.grbCanva.Name = "grbCanva";
            this.grbCanva.Size = new System.Drawing.Size(417, 426);
            this.grbCanva.TabIndex = 11;
            this.grbCanva.TabStop = false;
            this.grbCanva.Text = "Dibujo";
            // 
            // grbOutputs
            // 
            this.grbOutputs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grbOutputs.Controls.Add(this.txtArea);
            this.grbOutputs.Controls.Add(this.txtPerimeter);
            this.grbOutputs.Controls.Add(this.lblArea);
            this.grbOutputs.Controls.Add(this.lblPerimeter);
            this.grbOutputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOutputs.Location = new System.Drawing.Point(13, 322);
            this.grbOutputs.Name = "grbOutputs";
            this.grbOutputs.Size = new System.Drawing.Size(352, 116);
            this.grbOutputs.TabIndex = 10;
            this.grbOutputs.TabStop = false;
            this.grbOutputs.Text = "Salidas";
            // 
            // txtArea
            // 
            this.txtArea.Enabled = false;
            this.txtArea.Location = new System.Drawing.Point(130, 68);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(138, 24);
            this.txtArea.TabIndex = 3;
            // 
            // txtPerimeter
            // 
            this.txtPerimeter.Enabled = false;
            this.txtPerimeter.Location = new System.Drawing.Point(130, 31);
            this.txtPerimeter.Name = "txtPerimeter";
            this.txtPerimeter.Size = new System.Drawing.Size(138, 24);
            this.txtPerimeter.TabIndex = 2;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(82, 71);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(42, 18);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Área:";
            // 
            // lblPerimeter
            // 
            this.lblPerimeter.AutoSize = true;
            this.lblPerimeter.Location = new System.Drawing.Point(47, 31);
            this.lblPerimeter.Name = "lblPerimeter";
            this.lblPerimeter.Size = new System.Drawing.Size(77, 18);
            this.lblPerimeter.TabIndex = 0;
            this.lblPerimeter.Text = "Perímetro:";
            // 
            // grbProcess
            // 
            this.grbProcess.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grbProcess.Controls.Add(this.btnExit);
            this.grbProcess.Controls.Add(this.btnReset);
            this.grbProcess.Controls.Add(this.btnCalculate);
            this.grbProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbProcess.Location = new System.Drawing.Point(12, 228);
            this.grbProcess.Name = "grbProcess";
            this.grbProcess.Size = new System.Drawing.Size(353, 88);
            this.grbProcess.TabIndex = 9;
            this.grbProcess.TabStop = false;
            this.grbProcess.Text = "Proceso";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.PowderBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(238, 32);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 32);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.PowderBlue;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(120, 32);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(112, 32);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Limpiar";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.PowderBlue;
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCalculate.Location = new System.Drawing.Point(6, 32);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(108, 32);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Calcular";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // grbInputs
            // 
            this.grbInputs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grbInputs.Controls.Add(this.txtSide4);
            this.grbInputs.Controls.Add(this.lblSide4);
            this.grbInputs.Controls.Add(this.txtSide3);
            this.grbInputs.Controls.Add(this.txtSide2);
            this.grbInputs.Controls.Add(this.txtSide1);
            this.grbInputs.Controls.Add(this.lblSide3);
            this.grbInputs.Controls.Add(this.lblSide2);
            this.grbInputs.Controls.Add(this.lblSide1);
            this.grbInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInputs.Location = new System.Drawing.Point(13, 12);
            this.grbInputs.Name = "grbInputs";
            this.grbInputs.Size = new System.Drawing.Size(352, 210);
            this.grbInputs.TabIndex = 8;
            this.grbInputs.TabStop = false;
            this.grbInputs.Text = "Entradas";
            // 
            // txtSide3
            // 
            this.txtSide3.Location = new System.Drawing.Point(108, 113);
            this.txtSide3.Name = "txtSide3";
            this.txtSide3.Size = new System.Drawing.Size(149, 24);
            this.txtSide3.TabIndex = 9;
            // 
            // txtSide2
            // 
            this.txtSide2.Location = new System.Drawing.Point(108, 76);
            this.txtSide2.Name = "txtSide2";
            this.txtSide2.Size = new System.Drawing.Size(149, 24);
            this.txtSide2.TabIndex = 8;
            // 
            // txtSide1
            // 
            this.txtSide1.Location = new System.Drawing.Point(108, 36);
            this.txtSide1.Name = "txtSide1";
            this.txtSide1.Size = new System.Drawing.Size(149, 24);
            this.txtSide1.TabIndex = 7;
            // 
            // txtSide4
            // 
            this.txtSide4.Location = new System.Drawing.Point(108, 148);
            this.txtSide4.Name = "txtSide4";
            this.txtSide4.Size = new System.Drawing.Size(149, 24);
            this.txtSide4.TabIndex = 5;
            // 
            // lblSide3
            // 
            this.lblSide3.AutoSize = true;
            this.lblSide3.Location = new System.Drawing.Point(47, 113);
            this.lblSide3.Name = "lblSide3";
            this.lblSide3.Size = new System.Drawing.Size(57, 18);
            this.lblSide3.TabIndex = 4;
            this.lblSide3.Text = "Lado 3:";
            // 
            // lblSide2
            // 
            this.lblSide2.AutoSize = true;
            this.lblSide2.Location = new System.Drawing.Point(47, 76);
            this.lblSide2.Name = "lblSide2";
            this.lblSide2.Size = new System.Drawing.Size(57, 18);
            this.lblSide2.TabIndex = 3;
            this.lblSide2.Text = "Lado 2:";
            // 
            // lblSide1
            // 
            this.lblSide1.AutoSize = true;
            this.lblSide1.Location = new System.Drawing.Point(47, 36);
            this.lblSide1.Name = "lblSide1";
            this.lblSide1.Size = new System.Drawing.Size(57, 18);
            this.lblSide1.TabIndex = 2;
            this.lblSide1.Text = "Lado 1:";
            // 
            // lblSide4
            // 
            this.lblSide4.AutoSize = true;
            this.lblSide4.Location = new System.Drawing.Point(47, 151);
            this.lblSide4.Name = "lblSide4";
            this.lblSide4.Size = new System.Drawing.Size(57, 18);
            this.lblSide4.TabIndex = 0;
            this.lblSide4.Text = "Lado 4:";
            // 
            // FrmTrapezoid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grbCanva);
            this.Controls.Add(this.grbOutputs);
            this.Controls.Add(this.grbProcess);
            this.Controls.Add(this.grbInputs);
            this.Name = "FrmTrapezoid";
            this.Text = "Trapezoide";
            this.Load += new System.EventHandler(this.FrmTrapezoid_Load);
            this.grbOutputs.ResumeLayout(false);
            this.grbOutputs.PerformLayout();
            this.grbProcess.ResumeLayout(false);
            this.grbInputs.ResumeLayout(false);
            this.grbInputs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbCanva;
        private System.Windows.Forms.GroupBox grbOutputs;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.TextBox txtPerimeter;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblPerimeter;
        private System.Windows.Forms.GroupBox grbProcess;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.GroupBox grbInputs;
        private System.Windows.Forms.TextBox txtSide3;
        private System.Windows.Forms.TextBox txtSide2;
        private System.Windows.Forms.TextBox txtSide1;
        private System.Windows.Forms.TextBox txtSide4;
        private System.Windows.Forms.Label lblSide3;
        private System.Windows.Forms.Label lblSide2;
        private System.Windows.Forms.Label lblSide1;
        private System.Windows.Forms.Label lblSide4;
    }
}