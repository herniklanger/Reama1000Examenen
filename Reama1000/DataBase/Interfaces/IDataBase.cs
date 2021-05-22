using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDataBase<T, TId> where T : class
    {
        Task<List<T>> ReadAllAsync();
        Task<List<T>> ReadAsync(Func<T, bool> search);
        Task<T> UpdateAsync(T obj);
        Task CreateAsync(T obj);
        Task CreateAsync(T[] obj);
        Task Delete(TId search);
    }
}