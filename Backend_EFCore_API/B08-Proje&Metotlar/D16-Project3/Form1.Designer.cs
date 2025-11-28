namespace D16_Project3
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
            dgrwCustomers = new DataGridView();
            tbxId = new TextBox();
            tbxFirstName = new TextBox();
            tbxLastName = new TextBox();
            tbxEmail = new TextBox();
            tbxCity = new TextBox();
            lblId = new Label();
            lblFirstName = new Label();
            lblLastName = new Label();
            lblEmail = new Label();
            lblCity = new Label();
            btnAdd = new Button();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgrwCustomers).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgrwCustomers
            // 
            dgrwCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrwCustomers.Dock = DockStyle.Top;
            dgrwCustomers.Location = new Point(0, 0);
            dgrwCustomers.Name = "dgrwCustomers";
            dgrwCustomers.RowHeadersWidth = 51;
            dgrwCustomers.Size = new Size(800, 188);
            dgrwCustomers.TabIndex = 0;
            // 
            // tbxId
            // 
            tbxId.Font = new Font("Segoe UI", 12F);
            tbxId.Location = new Point(99, 26);
            tbxId.Name = "tbxId";
            tbxId.Size = new Size(189, 34);
            tbxId.TabIndex = 1;
            // 
            // tbxFirstName
            // 
            tbxFirstName.Font = new Font("Segoe UI", 12F);
            tbxFirstName.Location = new Point(99, 83);
            tbxFirstName.Name = "tbxFirstName";
            tbxFirstName.Size = new Size(189, 34);
            tbxFirstName.TabIndex = 1;
            // 
            // tbxLastName
            // 
            tbxLastName.Font = new Font("Segoe UI", 12F);
            tbxLastName.Location = new Point(99, 130);
            tbxLastName.Name = "tbxLastName";
            tbxLastName.Size = new Size(189, 34);
            tbxLastName.TabIndex = 1;
            // 
            // tbxEmail
            // 
            tbxEmail.Font = new Font("Segoe UI", 12F);
            tbxEmail.Location = new Point(99, 185);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(189, 34);
            tbxEmail.TabIndex = 1;
            // 
            // tbxCity
            // 
            tbxCity.Font = new Font("Segoe UI", 12F);
            tbxCity.Location = new Point(99, 241);
            tbxCity.Name = "tbxCity";
            tbxCity.Size = new Size(189, 34);
            tbxCity.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 12F);
            lblId.Location = new Point(26, 36);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 28);
            lblId.TabIndex = 2;
            lblId.Text = "Id";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 12F);
            lblFirstName.Location = new Point(14, 88);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(42, 28);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "Adı";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 12F);
            lblLastName.Location = new Point(14, 135);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(72, 28);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Soyadı";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F);
            lblEmail.Location = new Point(14, 185);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(50, 28);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Mail";
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Segoe UI", 12F);
            lblCity.Location = new Point(14, 241);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(56, 28);
            lblCity.TabIndex = 2;
            lblCity.Text = "Şehir";
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnAdd.Location = new Point(146, 281);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(142, 44);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
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
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            groupBox1.Location = new Point(0, 194);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(415, 343);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ekle";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 539);
            Controls.Add(groupBox1);
            Controls.Add(dgrwCustomers);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgrwCustomers).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgrwCustomers;
        private TextBox tbxId;
        private TextBox tbxFirstName;
        private TextBox tbxLastName;
        private TextBox tbxEmail;
        private TextBox tbxCity;
        private Label lblId;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblEmail;
        private Label lblCity;
        private Button btnAdd;
        private GroupBox groupBox1;
    }
}
