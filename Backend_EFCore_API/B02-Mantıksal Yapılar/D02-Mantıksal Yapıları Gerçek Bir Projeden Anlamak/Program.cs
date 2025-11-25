namespace D02_Mantıksal_Yapıları_Gerçek_Bir_Projeden_Anlamak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool deger = true;

            double piyasaDun = 115000.6;
            double piyasaBugun = 116000.6;
            bool sonuc = piyasaBugun == piyasaDun;

            string mesajArtis = "Artış oku";
            string mesajAzalis = "Azalış oku";
            string mesajAyni = "Sabit oku";
            if (piyasaBugun>piyasaDun)
            {
                Console.WriteLine(mesajArtis);
            }
            else if (piyasaBugun==piyasaDun) 
            {
                Console.WriteLine(mesajAyni);
            }
            else
            {
                Console.WriteLine(mesajAzalis);
            }
        }
    }
}
