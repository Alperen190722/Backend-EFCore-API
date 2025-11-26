namespace D09_Arabalar_Simulasyon_Ödevi
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
            lbxProducts = new ListBox();
            lblProducts = new Label();
            btnAddToCart = new Button();
            lblCart = new Label();
            lbxCart = new ListBox();
            btnRemoveFromCart = new Button();
            SuspendLayout();
            // 
            // lbxProducts
            // 
            lbxProducts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lbxProducts.FormattingEnabled = true;
            lbxProducts.Location = new Point(30, 78);
            lbxProducts.Name = "lbxProducts";
            lbxProducts.Size = new Size(150, 200);
            lbxProducts.TabIndex = 0;
            // 
            // lblProducts
            // 
            lblProducts.AutoSize = true;
            lblProducts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblProducts.Location = new Point(30, 47);
            lblProducts.Name = "lblProducts";
            lblProducts.Size = new Size(86, 28);
            lblProducts.TabIndex = 1;
            lblProducts.Text = "Arabalar";
            // 
            // btnAddToCart
            // 
            btnAddToCart.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnAddToCart.Location = new Point(197, 78);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(149, 57);
            btnAddToCart.TabIndex = 2;
            btnAddToCart.Text = "Sepete Ekle";
            btnAddToCart.UseVisualStyleBackColor = true;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // lblCart
            // 
            lblCart.AutoSize = true;
            lblCart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblCart.Location = new Point(391, 56);
            lblCart.Name = "lblCart";
            lblCart.Size = new Size(92, 28);
            lblCart.TabIndex = 3;
            lblCart.Text = "Sepetiniz";
            // 
            // lbxCart
            // 
            lbxCart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lbxCart.FormattingEnabled = true;
            lbxCart.Location = new Point(391, 87);
            lbxCart.Name = "lbxCart";
            lbxCart.Size = new Size(143, 200);
            lbxCart.TabIndex = 4;
            // 
            // btnRemoveFromCart
            // 
            btnRemoveFromCart.Location = new Point(553, 87);
            btnRemoveFromCart.Name = "btnRemoveFromCart";
            btnRemoveFromCart.Size = new Size(152, 48);
            btnRemoveFromCart.TabIndex = 5;
            btnRemoveFromCart.Text = "Sepetten çıkar";
            btnRemoveFromCart.UseVisualStyleBackColor = true;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRemoveFromCart);
            Controls.Add(lbxCart);
            Controls.Add(lblCart);
            Controls.Add(btnAddToCart);
            Controls.Add(lblProducts);
            Controls.Add(lbxProducts);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbxProducts;
        private Label lblProducts;
        private Button btnAddToCart;
        private Label lblCart;
        private ListBox lbxCart;
        private Button btnRemoveFromCart;
    }
}
