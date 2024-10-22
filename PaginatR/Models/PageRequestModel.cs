using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaginatR.Models
{
    public class PageRequestModel<TModel>
    {
        public IEnumerable<Expression<Func<TModel, bool>>> Filters { get; set; }

        public IEnumerable<OrderByModel<TModel>> Orderings { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PageRequestModel(
            IEnumerable<Expression<Func<TModel, bool>>> filters,
            IEnumerable<OrderByModel<TModel>> orderings,
            int pageNumber,
            int pageSize)
        {
            Filters = filters;
            Orderings = orderings;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PageRequestModel(int pageNumber, int pageSize)
            : this(Enumerable.Empty<Expression<Func<TModel, bool>>>(), Enumerable.Empty<OrderByModel<TModel>>(), pageNumber, pageSize)
        {
        }
    }
}
