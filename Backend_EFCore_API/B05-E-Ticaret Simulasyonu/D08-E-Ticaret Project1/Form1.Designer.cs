namespace E_Ticaret_Project
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
            lbxCart = new ListBox();
            lblCart = new Label();
            btnRemoveFromCart = new Button();
            SuspendLayout();
            // 
            // lbxProducts
            // 
            lbxProducts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lbxProducts.FormattingEnabled = true;
            lbxProducts.Location = new Point(12, 65);
            lbxProducts.Name = "lbxProducts";
            lbxProducts.Size = new Size(160, 228);
            lbxProducts.TabIndex = 0;
            // 
            // lblProducts
            // 
            lblProducts.AutoSize = true;
            lblProducts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblProducts.Location = new Point(12, 32);
            lblProducts.Name = "lblProducts";
            lblProducts.Size = new Size(77, 28);
            lblProducts.TabIndex = 1;
            lblProducts.Text = "Ürünler";
            lblProducts.Click += lblProducts_Click;
            // 
            // btnAddToCart
            // 
            btnAddToCart.Location = new Point(194, 65);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(179, 41);
            btnAddToCart.TabIndex = 2;
            btnAddToCart.Text = "button1";
            btnAddToCart.UseVisualStyleBackColor = true;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // lbxCart
            // 
            lbxCart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lbxCart.FormattingEnabled = true;
            lbxCart.Location = new Point(392, 65);
            lbxCart.Name = "lbxCart";
            lbxCart.Size = new Size(169, 228);
            lbxCart.TabIndex = 3;
            // 
            // lblCart
            // 
            lblCart.AutoSize = true;
            lblCart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblCart.Location = new Point(392, 32);
            lblCart.Name = "lblCart";
            lblCart.Size = new Size(65, 28);
            lblCart.TabIndex = 4;
            lblCart.Text = "label1";
            // 
            // btnRemoveFromCart
            // 
            btnRemoveFromCart.Location = new Point(591, 66);
            btnRemoveFromCart.Name = "btnRemoveFromCart";
            btnRemoveFromCart.Size = new Size(162, 40);
            btnRemoveFromCart.TabIndex = 5;
            btnRemoveFromCart.Text = "button1";
            btnRemoveFromCart.UseVisualStyleBackColor = true;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRemoveFromCart);
            Controls.Add(lblCart);
            Controls.Add(lbxCart);
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
        private ListBox lbxCart;
        private Label lblCart;
        private Button btnRemoveFromCart;
    }
}
