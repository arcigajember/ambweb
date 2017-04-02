using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.DataLayer.Interface
{
    public interface IDefaultRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectById(int? id);
        Task<int> Insert(T model);
        Task Delete(int? id);
        Task Update(T model);
    }
}
