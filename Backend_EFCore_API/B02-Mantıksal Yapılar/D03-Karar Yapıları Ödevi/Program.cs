using System.Threading.Channels;

namespace D03_Karar_Yapıları_Ödevi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sayi1 = 15;
            int sayi2 = 25;

            string SayiKontrol =
            (sayi1 > sayi2) ? "sayi1 büyüktür sayi2'den.." :
            (sayi2 > sayi1) ? "sayi2 büyüktür sayi1'den.." :
            "sayi1 eşittir sayi2'ye..";
            Console.WriteLine(SayiKontrol);
        }
    }
}
