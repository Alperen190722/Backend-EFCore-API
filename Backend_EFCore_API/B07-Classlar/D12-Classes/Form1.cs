namespace D12_Classes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<string> isimler = new List<string>() { "Alperen", "Furkan", "Semih", "Ali" };
            List<int> yaslar = new List<int>() { 22, 23, 26, 22 };
            List<string> mailler = new List<string>() { "alperen@asdasd.com", "furkan@sdasd.com", "semih@agrtk.com", "ali@ngjskl.com" };

            for (int i = 0; i < 4; i++)
            {
                //MessageBox.Show(isimler[i] + " " + yaslar[i] + " " + mailler[i]);
            }

            Student ogrenci1 = new Student();
            ogrenci1.FirstName = "Furkan";
            ogrenci1.Age = 23;
            ogrenci1.Mail = "furkan@adsadas.com";

            Student ogrenci2 = new Student();
            ogrenci2.FirstName = "Alperen";
            ogrenci2.Age = 22;
            ogrenci2.Mail = "alperen@adsadas.com";

            Student ogrenci3 = new Student();
            ogrenci3.FirstName = "Semih";
            ogrenci3.Age = 26;
            ogrenci3.Mail = "semih@adsadas.com";

            List<Student> students = new List<Student>() { ogrenci1, ogrenci2, ogrenci3 };

            foreach (var student in students)
            {
                //MessageBox.Show(student.FirstName + " " + student.Age);
                lbxStudents.Items.Add(student.FirstName);
                lbxStudents.Items.Add(student.Age);
                lbxStudents.Items.Add(student.Mail);
            }

            dgrwStudents.DataSource = students;
        }
    }
}
