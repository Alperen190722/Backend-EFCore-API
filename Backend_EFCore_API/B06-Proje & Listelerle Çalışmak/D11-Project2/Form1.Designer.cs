namespace D11_Project2
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
            lbxStudentList = new ListBox();
            lblStudentList = new Label();
            lblStudentName = new Label();
            tbxStudentName = new TextBox();
            btnAddStudent = new Button();
            btnRemoveStudent = new Button();
            SuspendLayout();
            // 
            // lbxStudentList
            // 
            lbxStudentList.FormattingEnabled = true;
            lbxStudentList.Location = new Point(21, 102);
            lbxStudentList.Name = "lbxStudentList";
            lbxStudentList.Size = new Size(150, 244);
            lbxStudentList.TabIndex = 0;
            // 
            // lblStudentList
            // 
            lblStudentList.AutoSize = true;
            lblStudentList.Location = new Point(21, 67);
            lblStudentList.Name = "lblStudentList";
            lblStudentList.Size = new Size(105, 20);
            lblStudentList.TabIndex = 1;
            lblStudentList.Text = "Öğrenci Listesi";
            // 
            // lblStudentName
            // 
            lblStudentName.AutoSize = true;
            lblStudentName.Font = new Font("Segoe UI", 12F);
            lblStudentName.Location = new Point(285, 108);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(116, 28);
            lblStudentName.TabIndex = 2;
            lblStudentName.Text = "Öğrenci Adı";
            // 
            // tbxStudentName
            // 
            tbxStudentName.Font = new Font("Segoe UI", 12F);
            tbxStudentName.Location = new Point(427, 105);
            tbxStudentName.Name = "tbxStudentName";
            tbxStudentName.Size = new Size(292, 34);
            tbxStudentName.TabIndex = 3;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Font = new Font("Segoe UI", 12F);
            btnAddStudent.Location = new Point(561, 166);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(158, 44);
            btnAddStudent.TabIndex = 4;
            btnAddStudent.Text = "Öğrenci Ekle";
            btnAddStudent.UseVisualStyleBackColor = true;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // btnRemoveStudent
            // 
            btnRemoveStudent.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnRemoveStudent.Location = new Point(32, 370);
            btnRemoveStudent.Name = "btnRemoveStudent";
            btnRemoveStudent.Size = new Size(139, 38);
            btnRemoveStudent.TabIndex = 5;
            btnRemoveStudent.Text = "Öğrenci Sil";
            btnRemoveStudent.UseVisualStyleBackColor = true;
            btnRemoveStudent.Click += btnRemoveStudent_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRemoveStudent);
            Controls.Add(btnAddStudent);
            Controls.Add(tbxStudentName);
            Controls.Add(lblStudentName);
            Controls.Add(lblStudentList);
            Controls.Add(lbxStudentList);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbxStudentList;
        private Label lblStudentList;
        private Label lblStudentName;
        private TextBox tbxStudentName;
        private Button btnAddStudent;
        private Button btnRemoveStudent;
    }
}
