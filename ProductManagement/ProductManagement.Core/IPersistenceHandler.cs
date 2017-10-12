using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core
{
    public interface IPersistenceHandler<T> where T : class
    {
        void Add(T input);
        void Edit(T input);
        void Remove(T input);

        T Get(Func<T, bool> criteria);
        List<T> GetAll(Func<T, bool> criteria = null);
    }
}
