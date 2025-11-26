namespace D10_Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] sehirler = new string[] { "İstanbul", "Ankara", "Bursa" };
            Console.WriteLine(sehirler.Length);

            sehirler = new string[4];
            sehirler[3] = "İzmir";
            foreach (var sehir in sehirler)
            {
                Console.WriteLine(sehir);
            }

            List<string> sehirler2 = new List<string>() { "İstanbul", "Ankara", "Bursa" };
            Console.WriteLine(sehirler2.Count);
            sehirler2.Add("İzmir");

            foreach (var sehir in sehirler2)
            {
                Console.WriteLine(sehir);
            }
            sehirler2.Add("Antalya");
            sehirler2.Remove("İzmir");
            bool sonuc = sehirler2.Contains("İstanbul");

            
        }
    }
}
