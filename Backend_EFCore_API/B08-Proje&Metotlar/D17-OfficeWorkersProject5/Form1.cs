namespace D17_OfficeWorkersProject5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WorkerManager workerManager = new WorkerManager();
        private void Form1_Load(object sender, EventArgs e)
        {
            dgrwWorkers.DataSource = workerManager.GetAll();
            dgrwWorkers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Worker worker = new Worker();
            worker.Id = int.Parse(tbxId.Text);
            worker.FirstName = tbxFirstName.Text;
            worker.LastName = tbxLastName.Text;
            worker.Email = tbxEmail.Text;
            worker.City = tbxCity.Text;

            workerManager.Add(worker);

            dgrwWorkers.DataSource = null;
            dgrwWorkers.DataSource = workerManager.GetAll();

            MessageBox.Show("Baþarýyla eklendi!");

        }
    }
}
