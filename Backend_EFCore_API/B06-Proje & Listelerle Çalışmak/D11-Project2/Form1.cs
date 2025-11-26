namespace D11_Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> students;
        private void Form1_Load(object sender, EventArgs e)
        {
            students = new List<string>() { "Alperen Piþkin", "Semih Tecer", "Furkan Sülek" };

            foreach (var student in students)
            {
                lbxStudentList.Items.Add(student);
            }

        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (tbxStudentName.Text.Length >= 2)
            {
                
                students.Add(tbxStudentName.Text);
                lbxStudentList.Items.Clear();

                foreach (var student in students)
                {
                    lbxStudentList.Items.Add(student);
                }

            }
            else
            {
                MessageBox.Show("Öðrenci ismi en az iki karakter olmalýdýr");
            }
        }

        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            if(lbxStudentList.SelectedItem == null)
            {
                MessageBox.Show("Öncelikle bir öðrenci seçmelisiniz");
            }
            else 
            {
                students.Remove(lbxStudentList.SelectedItem.ToString());
            }

            lbxStudentList.Items.Clear();

            foreach (var student in students)
            {
                lbxStudentList.Items.Add(student);
            }

        }
    }
}
