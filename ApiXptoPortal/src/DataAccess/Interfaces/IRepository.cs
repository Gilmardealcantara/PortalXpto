using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XptoPortalApi.Models;
using Users.DataAccess;

namespace XptoPortalApi.DataAcess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<Boolean> Insert(T obj);

        Task<Boolean> Update(T obj);

        Task<Boolean> Delete(int id);

        Task<T> Select(int id);

        IAsyncEnumerable<T> Select();

        IAsyncEnumerable<T> GetBy(Func<T, bool> predicate);

        Task<PagedResult<T>> GetPaged(IAsyncEnumerable<T> query, int page, int pageSize);
    }
}

