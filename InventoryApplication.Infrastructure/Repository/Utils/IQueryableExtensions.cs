using InventoryApplication.Domain.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Infrastructure.Repository.Utils
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static async Task<Paged<T>> ToPagedAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Paged<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
