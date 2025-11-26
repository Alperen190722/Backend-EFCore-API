namespace D06_Döngüler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sayi = 10;
            var sayi2 = 10;

            string[] krediler = new string[7];
            krediler[0] = "Hızlı Kredi";
            krediler[1] = "Kredi 2";
            krediler[2] = "Kredi 3";
            krediler[3] = "Kredi 4";
            krediler[4] = "Kredi 5";
            krediler[5] = "Kredi 6";
            krediler[6] = "Kredi 7";
            krediler[7] = "Kredi 8";


            for (int i = 0; i < krediler.Length; i++) 
            {
                Console.WriteLine(krediler[i]);
            }

            Console.WriteLine("For döngüsü bitti");

            foreach (string kredi in krediler)
            {
                Console.WriteLine(kredi);
            }

            Console.WriteLine("Bitti");

            int sayac = 0;
            while (sayac < 7)
            {
                Console.WriteLine(krediler[sayac]);
                sayac++;
            }

            int sayac2 = 0;
            do
            {
              Console.WriteLine(krediler[sayac2]);
              sayac2++;
            } while (sayac2 < 7);
        }
    }
}
