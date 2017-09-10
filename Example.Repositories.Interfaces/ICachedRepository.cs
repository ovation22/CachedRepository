using System;
using System.Linq;

namespace Example.Repositories.Interfaces
{
    public interface ICachedRepository<T>
    {
        T Get(Func<T, bool> predicate);
        IQueryable<T> GetAll();       
    }
}