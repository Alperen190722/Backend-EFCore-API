namespace D33_ThreadingDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show($"Thread no : {Thread.CurrentThread.ManagedThreadId}");
        }

        private void btnProcess1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            MessageBox.Show($"Thread no : {Thread.CurrentThread.ManagedThreadId}");
        }

        private void btnProcess2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Thread no : {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
