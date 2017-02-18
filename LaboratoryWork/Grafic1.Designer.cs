namespace LaboratoryWork
{
    partial class Grafic1
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
            this.imageBox = new GraphBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxNew1
            // 
            this.imageBox.Coef_X_Cartesian = 0F;
            this.imageBox.Coef_X_Polar = 0F;
            this.imageBox.Coef_Y_Cartesian = 0F;
            this.imageBox.Coef_Y_Polar = 0F;
            this.imageBox.CoordinateSystem = LaboratoryWork.Enums.TypeCoordinateSystem.Cartesian;
            this.imageBox.CountLineX = 0;
            this.imageBox.CountLineY = 0;
            this.imageBox.Degrees = 0;
            this.imageBox.HeightNew = 0;
            this.imageBox.Location = new System.Drawing.Point(12, 47);
            this.imageBox.Name = "imageBoxNew1";
            this.imageBox.Size = new System.Drawing.Size(580, 295);
            this.imageBox.StepRadius = 0F;
            this.imageBox.TabIndex = 3;
            this.imageBox.TabStop = false;
            this.imageBox.X0_Dec = 0;
            this.imageBox.X0_Pol = 0;
            this.imageBox.Y0_Dec = 0;
            this.imageBox.Y0_Pol = 0;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBoxNew1_Paint);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(36, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(317, 15);
            this.label23.TabIndex = 52;
            this.label23.Text = "Нормированная ДН в плоскости, перпендикулярной витку";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Полярный угол";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Значение ДН по радиальному направлению";
            // 
            // Grafic1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 353);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.imageBox);
            this.Name = "Grafic1";
            this.Text = "Grafic1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GraphBox imageBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}