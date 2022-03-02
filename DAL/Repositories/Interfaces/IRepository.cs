using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T value);

        Task<T> Update(T value);

        Task<bool> Delete(object key);

        Task<ICollection<T>> ReadAll();
        
        Task<ICollection<T>> ReadAllInclude();
        
        Task<ICollection<T>> GetBySelector(Func<T, bool> selector);

        Task<T> GetById(object id);
    }
}