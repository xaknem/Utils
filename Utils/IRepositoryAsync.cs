using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// Интерфейс обёртки для базы данных 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryAsync<T>  where T : class 
    {
        T Get(Func<T, bool> predicate);
        Task<ICollection<T>> GetManyAsync(Func<T, bool> predicate);
        void Add(T item);
        void AddMany(ICollection<T> items);
        void Edit(T item);
        void Drop(T item);   
    }
}