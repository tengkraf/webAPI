using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions
{
    public static class GeneralEntityExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var resultsTask = query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var countTask = query.CountAsync();

            await Task.WhenAll(resultsTask, countTask);

            var result = new PagedResult<T>
            {
                Results = await resultsTask,
                TotalRowCount = await countTask
            };

            return result;
        }

    }
}
