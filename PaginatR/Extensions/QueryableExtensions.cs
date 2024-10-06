using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using PaginatR.Enums;
using PaginatR.Models;

namespace PaginatR.Extensions
{
    public static class QueryableExtensions
    {
        public static PageResultModel<TModel> ApplyPageRequest<TModel>(this IQueryable<TModel> queryable, PageRequestModel<TModel> pageRequest)
        {
            queryable = queryable
                .ApplyFilters(pageRequest.Filters)
                .ApplyOrdering(pageRequest.Orderings);

            var totalCount = queryable.LongCount();

            queryable = queryable.ApplyPaging(pageRequest.PageNumber, pageRequest.PageSize);
            return new PageResultModel<TModel>(queryable, totalCount);
        }

        public static IQueryable<TModel> ApplyPaging<TModel>(this IQueryable<TModel> queryable, int pageNumber, int pageSize)
        {
            return queryable
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<TModel> ApplyOrdering<TModel>(this IQueryable<TModel> queryable, IEnumerable<OrderByModel<TModel>> orderings)
        {
            foreach (var ordering in orderings)
            {
                queryable = ordering.Direction switch
                {
                    OrderDirection.Ascending => queryable.OrderBy(ordering.Property),
                    OrderDirection.Descending => queryable.OrderByDescending(ordering.Property),
                    _ => throw new NotSupportedException()
                };
            }
            return queryable;
        }

        public static IQueryable<TModel> ApplyFilters<TModel>(this IQueryable<TModel> queryable, IEnumerable<Expression<Func<TModel, bool>>> filters)
        {
            foreach (var filter in filters)
            {
                queryable = queryable.Where(filter);
            }
            return queryable;
        }
    }
}
