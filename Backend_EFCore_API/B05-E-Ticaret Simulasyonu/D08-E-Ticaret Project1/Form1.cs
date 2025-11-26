namespace E_Ticaret_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void lblProducts_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var productsText = "Ürünler";
            var addToCartButtonText = "Sepete Ekle";
            var cartText = "Sepetiniz";
            var removeFromCartButtonText = "Sepetten çýkar";

            lblProducts.Text = productsText;
            btnAddToCart.Text = addToCartButtonText;
            lblCart.Text = cartText;
            btnRemoveFromCart.Text = removeFromCartButtonText;

            string[] products = new string[] { "Laptop", "Masaüstü PC", "Klavye" };

            //for (int i = 0; i < products.Length; i++)
            //{
            //    lbxProducts.Items.Add(products[i]);
            //}

            foreach (var item in products)
            {
                lbxProducts.Items.Add(item);
            }

            if (lbxCart.Items.Count == 0)
            {
                btnRemoveFromCart.Enabled = false;
            }

        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(lbxProducts.SelectedItem.ToString());

            if (lbxProducts.SelectedItem != null)
            {
                lbxCart.Items.Add(lbxProducts.SelectedItem);
                btnRemoveFromCart.Enabled = true;
            }
            else
            {
                MessageBox.Show("Öncelikle bir ürün seçmelisiniz!");
            }

            if (lbxProducts.Items.Count == 0)
            {
                btnAddToCart.Enabled = false;
            }
            lbxProducts.Items.Remove(lbxProducts.SelectedItem);
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (lbxCart.SelectedItem != null)
            {
                lbxProducts.Items.Add(lbxCart.SelectedItem);
                btnAddToCart.Enabled = true;
            }
            else
            {
                MessageBox.Show("Öncelikle bir ürün seçmelisiniz!");
            }

            if (lbxCart.Items.Count == 0) 
            {
                btnRemoveFromCart.Enabled = false;
            }
            lbxCart.Items.Remove(lbxCart.SelectedItem);
        }
    }
}
