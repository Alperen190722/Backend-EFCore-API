namespace D13_CarsÖdevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Car araba1 = new Car();
            araba1.Marka = "Rolls Royce";
            araba1.Model = "Ghost";
            araba1.ID = 01;

            Car araba2 = new Car();
            araba2.Marka = "Ferari";
            araba2.Model = "F10";
            araba2.ID = 02;

            Car araba3 = new Car();
            araba3.Marka = "Mercedes";
            araba3.Model = "Maybach";
            araba3.ID = 03;

            List<Car> cars = new List<Car>() { araba1, araba2, araba3 };

            foreach (var car in cars)
            {
                //MessageBox.Show(student.FirstName + " " + student.Age);
                lbxCars.Items.Add(car.Marka);
                lbxCars.Items.Add(car.Model);
                lbxCars.Items.Add(car.ID);
            }

            dgrwCars.DataSource = cars;
        }
    }
}
