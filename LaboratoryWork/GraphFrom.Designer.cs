namespace LaboratoryWork
{
    partial class GraphFrom
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
            this.lblNameFunction = new System.Windows.Forms.Label();
            this.lblSecondCoordinate = new System.Windows.Forms.Label();
            this.lblFirstCoordinate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
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
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(580, 295);
            this.imageBox.StepRadius = 0F;
            this.imageBox.TabIndex = 3;
            this.imageBox.TabStop = false;
            this.imageBox.X0_Dec = 0;
            this.imageBox.X0_Pol = 0;
            this.imageBox.Y0_Dec = 0;
            this.imageBox.Y0_Pol = 0;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBoxNew1_Paint);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox_Move);
            // 
            // label23
            // 
            this.lblNameFunction.Location = new System.Drawing.Point(36, 9);
            this.lblNameFunction.Name = "label23";
            this.lblNameFunction.Size = new System.Drawing.Size(317, 15);
            this.lblNameFunction.TabIndex = 52;
            this.lblNameFunction.Text = "Нормированная ДН в плоскости, перпендикулярной витку";
            // 
            // lblSecondCoordinate
            // 
            this.lblSecondCoordinate.AutoSize = true;
            this.lblSecondCoordinate.Location = new System.Drawing.Point(287, 31);
            this.lblSecondCoordinate.Name = "lblSecondCoordinate";
            this.lblSecondCoordinate.Size = new System.Drawing.Size(84, 13);
            this.lblSecondCoordinate.TabIndex = 51;
            this.lblSecondCoordinate.Text = "Полярный угол";
            // 
            // lblFirstCoordinate
            // 
            this.lblFirstCoordinate.AutoSize = true;
            this.lblFirstCoordinate.Location = new System.Drawing.Point(10, 30);
            this.lblFirstCoordinate.Name = "lblFirstCoordinate";
            this.lblFirstCoordinate.Size = new System.Drawing.Size(231, 13);
            this.lblFirstCoordinate.TabIndex = 50;
            this.lblFirstCoordinate.Text = "Значение ДН по радиальному направлению";
            // 
            // Grafic1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 353);
            this.Controls.Add(this.lblNameFunction);
            this.Controls.Add(this.lblSecondCoordinate);
            this.Controls.Add(this.lblFirstCoordinate);
            this.Controls.Add(this.imageBox);
            this.Name = "Grafic1";
            this.Text = "Grafic1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GraphBox imageBox;
        private System.Windows.Forms.Label lblNameFunction;
        private System.Windows.Forms.Label lblSecondCoordinate;
        private System.Windows.Forms.Label lblFirstCoordinate;
    }
}