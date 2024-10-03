using PaginatR.Dtos;
using System;
using System.Linq.Expressions;

namespace PaginatR.Adapters
{
    public interface IFilterByAdapter
    {
        Expression<Func<TModel, bool>> ConvertToExpression<TModel>(FilterByDto filterBy);
    }
}
