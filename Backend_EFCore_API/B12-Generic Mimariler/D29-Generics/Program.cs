
namespace D29_Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> sehirler = new List<string>();
            sehirler.Add("İstanbul");
            sehirler.Add("İstanbul");
            sehirler.Add("İstanbul");
            sehirler.Add("İstanbul");
            sehirler.Add("İstanbul");

            sehirler[0] = "Ankara"; //set
            Console.WriteLine(sehirler[0]); //get
            Console.WriteLine(sehirler.Count);


            MyList<string> sehirler2 = new MyList<string>();
            sehirler2.Add("İstanbul");
            sehirler2.Add("İstanbul");
            sehirler2.Add("İstanbul");
            sehirler2.Add("İstanbul");
            sehirler2.Add("İstanbul");
            sehirler2.Add("İstanbul");
            sehirler2[0] = "Ankara";
            //indexing
            Console.WriteLine(sehirler2.Count);
            //Console.WriteLine(sehirler2[0]); //get

            foreach (var sehir in sehirler2.Items)
            {
                Console.WriteLine(sehir);
            }
        }
    }

    class MyList<T> //Generic class
    {
        T[] _array;
        T[] _tempArray;
        public MyList()
        {
            _array = new T[0];
        }
        public void Add(T item) 
        {
            _tempArray = _array;
            _array = new T[_array.Length + 1];
            for (int i = 0; i < _tempArray.Length; i++)
            {
                _array[i] = _tempArray[i];
            }

            _array[_array.Length - 1] = item;
        }

        public int Count
        {
            get { return _array.Length; }
        }

        public T[] Items 
        {
            get 
            {
                return _array;
            } 
        }

        public T this[int index]
        {
            get 
            {
                return _array[index];
            }
            set
            {
                _array[index] = value;
            }
        }

    }
}
