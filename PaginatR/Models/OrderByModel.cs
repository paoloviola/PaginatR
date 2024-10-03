using System.Linq.Expressions;
using System;
using PaginatR.Enums;

namespace PaginatR.Models
{
    public class OrderByModel<TModel>
    {
        public Expression<Func<TModel, object>> Property { get; set; }

        public OrderDirection Direction { get; set; }

        public OrderByModel(Expression<Func<TModel, object>> property, OrderDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
