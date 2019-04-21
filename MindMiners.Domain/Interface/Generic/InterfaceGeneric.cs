using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindMiners.Domain.Interface.Generic
{
    public interface InterfaceGeneric<T> where T : class
    {
        void Add(T entity);
        IList<T> List();
    }
}
