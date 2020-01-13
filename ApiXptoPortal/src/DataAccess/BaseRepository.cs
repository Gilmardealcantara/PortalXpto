using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Models;
using Microsoft.EntityFrameworkCore;
using Users.DataAccess;

namespace XptoPortalApi.DataAcess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MainContext _context { get; private set; }
        public BaseRepository(MainContext context)
        {
            _context = context;
        }

        public virtual async Task<Boolean> Insert(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<Boolean> Update(T obj)
        {
            _context.Update(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<Boolean> Delete(int id)
        {
            _context.Set<T>().Remove(await Select(id));
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual IAsyncEnumerable<T> Select()
        {
            return _context.Set<T>().ToAsyncEnumerable();
        }

        public virtual async Task<T> Select(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IAsyncEnumerable<T> GetBy(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToAsyncEnumerable();
        }

        public async Task<PagedResult<T>> GetPaged(IAsyncEnumerable<T> query, int page, int pageSize)
        {

            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

