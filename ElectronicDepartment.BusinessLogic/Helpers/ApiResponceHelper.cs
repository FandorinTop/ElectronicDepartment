using ElectronicDepartment.Web.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElectronicDepartment.BusinessLogic.Helpers
{
    public static class ApiResponceHelper
    {
        public static async Task<ApiResponce<D>> CreateApiResponceAsync<T, D>(this IQueryable<T> dataQuery,
            Expression<Func<T, D>> selector,
            int take = 10,
            int skip = 0)
        {
            var result = dataQuery
                .Take(skip..take)
                .Select(selector);
            //.Filtering()
            //.Sorting();

            int count = await dataQuery.CountAsync();

            return new ApiResponce<D>()
            {
                Data = await result.ToListAsync(),
                HasNextPage = count >= skip * take + take,
                HasPreviewPage = 0 <= skip * take - take,
                TotalPage = count / take,
                PageIndex = skip,
                PageSize = take,
            };
        }
    }
}