using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D17_OfficeWorkersProject5
{
    internal class WorkerManager
    {
        public WorkerManager()
        {
            workers = new List<Worker>()
            {
                new Worker{Id = 1, FirstName = "Alperen", LastName = "Pişkin", City = "İstanbul", Email = "alperen@"},
                new Worker{Id = 2, FirstName = "Furkan", LastName = "Sülek", City = "Konya", Email = "furkan@"},
                new Worker{Id = 3, FirstName = "Semih", LastName = "Tecer", City = "Sivas", Email = "semih@"},
                new Worker{Id = 4, FirstName = "Ali", LastName = "Koçkan", City = "Tekirdağ", Email = "ali@"},
                new Worker{Id = 5, FirstName = "Belinay", LastName = "Pişkin", City = "Bursa", Email = "belinay@"},
            };
        }
        List<Worker> workers;

        public List<Worker> GetAll()
        {
            //Veritabanına bağlan

            return workers;
        }

        public void Add(Worker worker) 
        {
            workers.Add(worker);
        }
    }
}
