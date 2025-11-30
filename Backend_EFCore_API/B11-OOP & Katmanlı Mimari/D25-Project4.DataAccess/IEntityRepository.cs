using D28_Project4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D25_Project4.DataAccess
{
    //class -- refernce type
    public interface IEntityRepository<T> where T : class ,IEntity, new()
    {
        List<T> GetAll();
        List<T> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
