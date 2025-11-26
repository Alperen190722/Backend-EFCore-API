namespace D09_Arabalar_Simulasyon_Ödevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] products = new string[] { "Rolls Royce", "Ferrari", "Bugatti" };

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
            if (lbxProducts.SelectedItem != null)
            {
                lbxCart.Items.Add(lbxProducts.SelectedItem);
                btnRemoveFromCart.Enabled = true;
            }
            else 
            { 
                MessageBox.Show("Öncelikle bir araba seçmelisiniz!");
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
                MessageBox.Show("Öncelikle sepetten bir araba seçmelisiniz!");
            }
            if (lbxCart.Items.Count == 0) 
            { 
                btnRemoveFromCart.Enabled = false;
            }
            lbxCart.Items.Remove(lbxCart.SelectedItem);
        }
    }
}
