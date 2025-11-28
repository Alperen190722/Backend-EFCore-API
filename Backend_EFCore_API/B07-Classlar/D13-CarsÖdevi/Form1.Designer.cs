namespace D13_CarsÖdevi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbxCars = new ListBox();
            dgrwCars = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgrwCars).BeginInit();
            SuspendLayout();
            // 
            // lbxCars
            // 
            lbxCars.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lbxCars.FormattingEnabled = true;
            lbxCars.Location = new Point(34, 44);
            lbxCars.Name = "lbxCars";
            lbxCars.Size = new Size(150, 228);
            lbxCars.TabIndex = 0;
            // 
            // dgrwCars
            // 
            dgrwCars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrwCars.Location = new Point(261, 44);
            dgrwCars.Name = "dgrwCars";
            dgrwCars.RowHeadersWidth = 51;
            dgrwCars.Size = new Size(400, 228);
            dgrwCars.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgrwCars);
            Controls.Add(lbxCars);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgrwCars).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbxCars;
        private DataGridView dgrwCars;
    }
}
