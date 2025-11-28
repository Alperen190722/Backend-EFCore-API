namespace D17_OfficeWorkersProject5
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
            dgrwWorkers = new DataGridView();
            groupBox1 = new GroupBox();
            tbxId = new TextBox();
            btnAdd = new Button();
            tbxFirstName = new TextBox();
            lblCity = new Label();
            tbxLastName = new TextBox();
            lblEmail = new Label();
            tbxEmail = new TextBox();
            lblLastName = new Label();
            tbxCity = new TextBox();
            lblFirstName = new Label();
            lblId = new Label();
            ((System.ComponentModel.ISupportInitialize)dgrwWorkers).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgrwWorkers
            // 
            dgrwWorkers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrwWorkers.Dock = DockStyle.Top;
            dgrwWorkers.Location = new Point(0, 0);
            dgrwWorkers.Name = "dgrwWorkers";
            dgrwWorkers.RowHeadersWidth = 51;
            dgrwWorkers.Size = new Size(884, 188);
            dgrwWorkers.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbxId);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(tbxFirstName);
            groupBox1.Controls.Add(lblCity);
            groupBox1.Controls.Add(tbxLastName);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(tbxEmail);
            groupBox1.Controls.Add(lblLastName);
            groupBox1.Controls.Add(tbxCity);
            groupBox1.Controls.Add(lblFirstName);
            groupBox1.Controls.Add(lblId);
            groupBox1.Location = new Point(12, 194);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(375, 359);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ekle";
            // 
            // tbxId
            // 
            tbxId.Font = new Font("Segoe UI", 12F);
            tbxId.Location = new Point(99, 26);
            tbxId.Name = "tbxId";
            tbxId.Size = new Size(189, 34);
            tbxId.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnAdd.Location = new Point(146, 281);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(142, 44);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // tbxFirstName
            // 
            tbxFirstName.Font = new Font("Segoe UI", 12F);
            tbxFirstName.Location = new Point(99, 83);
            tbxFirstName.Name = "tbxFirstName";
            tbxFirstName.Size = new Size(189, 34);
            tbxFirstName.TabIndex = 5;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Segoe UI", 12F);
            lblCity.Location = new Point(14, 241);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(56, 28);
            lblCity.TabIndex = 9;
            lblCity.Text = "Şehir";
            // 
            // tbxLastName
            // 
            tbxLastName.Font = new Font("Segoe UI", 12F);
            tbxLastName.Location = new Point(99, 130);
            tbxLastName.Name = "tbxLastName";
            tbxLastName.Size = new Size(189, 34);
            tbxLastName.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F);
            lblEmail.Location = new Point(14, 185);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(50, 28);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Mail";
            // 
            // tbxEmail
            // 
            tbxEmail.Font = new Font("Segoe UI", 12F);
            tbxEmail.Location = new Point(99, 185);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(189, 34);
            tbxEmail.TabIndex = 7;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 12F);
            lblLastName.Location = new Point(14, 135);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(72, 28);
            lblLastName.TabIndex = 11;
            lblLastName.Text = "Soyadı";
            // 
            // tbxCity
            // 
            tbxCity.Font = new Font("Segoe UI", 12F);
            tbxCity.Location = new Point(99, 241);
            tbxCity.Name = "tbxCity";
            tbxCity.Size = new Size(189, 34);
            tbxCity.TabIndex = 8;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 12F);
            lblFirstName.Location = new Point(14, 88);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(42, 28);
            lblFirstName.TabIndex = 12;
            lblFirstName.Text = "Adı";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 12F);
            lblId.Location = new Point(26, 36);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 28);
            lblId.TabIndex = 13;
            lblId.Text = "Id";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 565);
            Controls.Add(groupBox1);
            Controls.Add(dgrwWorkers);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgrwWorkers).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgrwWorkers;
        private GroupBox groupBox1;
        private TextBox tbxId;
        private Button btnAdd;
        private TextBox tbxFirstName;
        private Label lblCity;
        private TextBox tbxLastName;
        private Label lblEmail;
        private TextBox tbxEmail;
        private Label lblLastName;
        private TextBox tbxCity;
        private Label lblFirstName;
        private Label lblId;
    }
}
