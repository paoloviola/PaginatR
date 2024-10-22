using PaginatR.Adapters.FilterBy;
using PaginatR.Adapters.OrderBy;
using PaginatR.Dtos;
using PaginatR.Models;
using System.Linq;

namespace PaginatR.Adapters.PageRequest
{
    public class PageRequestDtoToModelAdapter : IPageRequestDtoToModelAdapter
    {
        private readonly IFilterByDtoToExpressionAdapter _filterByAdapter;
        private readonly IOrderByDtoToModelAdapter _orderByAdapter;

        public PageRequestDtoToModelAdapter(
            IFilterByDtoToExpressionAdapter filterByAdapter,
            IOrderByDtoToModelAdapter orderByAdapter)
        {
            _filterByAdapter = filterByAdapter;
            _orderByAdapter = orderByAdapter;
        }

        public PageRequestDtoToModelAdapter()
            : this(new FilterByDtoToExpressionAdapter(), new OrderByDtoToModelAdapter())
        {
        }

        public PageRequestModel<TModel> ConvertToModel<TModel>(PageRequestDto pageRequest)
        {
            var filters = pageRequest.Filters
                .Select(filter => _filterByAdapter.ConvertToExpression<TModel>(filter));

            var orderings = pageRequest.Orderings
                .Select(ordering => _orderByAdapter.ConvertToModel<TModel>(ordering));

            return new PageRequestModel<TModel>(
                filters,
                orderings,
                pageRequest.PageNumber,
                pageRequest.PageSize);
        }
    }
}
