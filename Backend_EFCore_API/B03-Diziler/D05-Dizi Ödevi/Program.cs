namespace D05_Dizi_Ödevi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] sayilar = new int[3];
            sayilar[0] = 35;
            sayilar[1] = 42;
            sayilar[2] = 27;
            if (sayilar[1] > sayilar[0] && sayilar[1] > sayilar[2] && sayilar[0] > sayilar[2])
            {
                Console.WriteLine("En büyük sayı: " + sayilar[1]);
                Console.WriteLine("Orta sayı: " + sayilar[0]);
                Console.WriteLine("En küçük sayı: " + sayilar[2]);
            }
        }
    }
}
