namespace D15_Methods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matematik matematik = new Matematik();
            int sonuc1 = matematik.Topla(2, 3);
            int sonuc2 = matematik.Topla(2, 65, 68);
            int sonuc3 = matematik.Topla(sayi2: 65, sayi1: 10);

            int sonuc4 = matematik.ToplaParams(new int[] { 2, 3, 4, 5, 6 });
            int sonuc5 = matematik.ToplaParams(2,3,4,5,6,7,8);
        }
    }

    //overloading
    class Matematik
    {
        public int Topla(int sayi1, int sayi2)
        {
            return sayi1 + sayi2;
        }

        public int Cikar(int sayi1, int sayi2)
        {
            return sayi1 - sayi2;
        }

        public int ToplaParams(params int[] sayilar) 
        {
            int sonuc = 0;
            //foreach (var sayi in sayilar)
            //{
            //    sonuc = sonuc + sayi;
            //}

            return sayilar.Sum();
        }

        public int Topla(int sayi1, int sayi2, int sayi3)
        {
            return sayi1 + sayi2 + sayi3;
        }
    }
}
