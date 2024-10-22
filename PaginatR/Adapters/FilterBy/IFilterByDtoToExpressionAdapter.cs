using PaginatR.Dtos;
using System;
using System.Linq.Expressions;

namespace PaginatR.Adapters.FilterBy
{
    public interface IFilterByDtoToExpressionAdapter
    {
        Expression<Func<TModel, bool>> ConvertToExpression<TModel>(FilterByDto filterBy);
    }
}
