using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul
{
    public interface IDataMapper<T>
    {
        void Create(T Obj);
        T Read(long Key);
        void Update(T P);
        void Delete(T P);
    }
}
