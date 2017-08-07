using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCostmeticWAD2.BAL.Interfaces
{
    public interface IServices<T>
    {
        IList<T> GetAll();
        T GetByKey(int id);

        void Update(T obj);

        void Delete(int id);

        void Add(T obj);
    }
}
