using System.Linq.Dynamic.Core;
using ElectronicDepartment.Web.Shared.Common;

namespace ElectronicDepartment.BusinessLogic.Helpers
{
    public static class LinqDynamicOperationExtension
    {
        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> dataQuery, IEnumerable<Sort> sortings)
        {
            return dataQuery.OrderBy(SortString(sortings));
        }

        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> dataQuery, Sort sort)
        {
            return dataQuery.OrderBy(SortString(sort));
        }

        private static string SortString(IEnumerable<Sort> sortings, string result = "")
        {
            foreach (var sort in sortings)
            {
                result = SortString(sort, result);
            }

            return result;
        }

        private static string SortString(Sort sort, string result = "")
        {
            //TODO replace with stringBuilder
            if (!string.IsNullOrEmpty(result))
            {
                result += $", ";
            }

            result += $"{sort.Field} {sort.OrderBy}";

            return result;
        }

        public static IQueryable<T> Restrict<T>(this IQueryable<T> dataQuery, IEnumerable<Filter> filters)
        {
            return dataQuery.Where(RestrictionString(filters));
        }

        public static IQueryable<T> Restrict<T>(this IQueryable<T> dataQuery, Filter filter)
        {
            return dataQuery.Where(RestrictionString(filter));
        }

        private static string RestrictionString(IEnumerable<Filter> filters, string result = "")
        {
            foreach (var filter in filters)
            {
                result = RestrictionString(filter, result);
            }

            return result;
        }

        private static string RestrictionString(Filter filter, string result = "")
        {
            //TODO replace with stringBuilder
            if (!string.IsNullOrEmpty(result))
            {
                result += $" && ";
            }

            result += $"{filter.Field}=={filter.Value}";

            return result;
        }
    }
}