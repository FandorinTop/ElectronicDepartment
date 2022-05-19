using ElectronicDepartment.Web.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using ElectronicDepartment.Web.Shared.Common;

namespace ElectronicDepartment.BusinessLogic.Helpers
{
    public static class ApiResponceHelper
    {
        public static async Task<ApiResponce<D>> CreateApiResponceAsync<T, D>(this IQueryable<T> dataQuery,
            Expression<Func<T, D>> selector,
            IEnumerable<Filter> filters,
            IEnumerable<Sort> sorts,
            int take = 10,
            int skip = 0)
        {
            var result = dataQuery
                .Take(skip..take);                

            if (filters.Any())
            {
                result = result.Restrict(filters);
            }
            if (sorts.Any())
            {
                result = result.Sort(sorts);
            }

            var responce = result.Select(selector);
            int count = await dataQuery.CountAsync();

            return new ApiResponce<D>()
            {
                Data = await responce.ToListAsync(),
                HasNextPage = count >= skip * take + take,
                HasPreviewPage = 0 <= skip * take - take,
                TotalPage = count / take,
                PageIndex = skip,
                PageSize = take,
            };
        }
    }
}