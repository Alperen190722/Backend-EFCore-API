namespace D07_Arkadaş_Sayı_Kontrolü_Ödevi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Birinci sayıyı giriniz: ");
            string sayi1 = Console.ReadLine();
            int Sayi1 = int.Parse(sayi1);

           
            Console.Write("İkinci sayıyı giriniz: ");
            string sayi2 = Console.ReadLine();
            int Sayi2 = int.Parse(sayi2);

            int toplam1 = 0;
            int toplam2 = 0;

           
            for (int i = 1; i < Sayi1; i++)
            {
                
                if (Sayi1 % i == 0)
                {
                    toplam1 += i;
                }
            }


            for (int i = 1; i < Sayi2; i++)
            {
               
                if (Sayi2 % i == 0)
                {
                    toplam2 += i;
                }
            }

           
            if (Sayi1 != Sayi2 && toplam1 == Sayi2 && toplam2 == Sayi1)
            {
                Console.WriteLine("{0} ve {1} sayıları ARKADAŞ SAYILARDIR.",Sayi1,Sayi2);
            }
            else
            {
                Console.WriteLine("{0} ve {1} sayıları arkadaş sayı DEĞİLDİR.",Sayi1,Sayi2);
            }

            Console.ReadKey();
        }
    }
}
