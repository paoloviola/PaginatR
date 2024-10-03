using PaginatR.Dtos;
using PaginatR.Models;
using System.Linq;

namespace PaginatR.Adapters
{
    public class PageRequestAdapter : IPageRequestAdapter
    {
        private readonly IFilterByAdapter _filterByAdapter;
        private readonly IOrderByAdapter _orderByAdapter;

        public PageRequestAdapter(
            IFilterByAdapter filterByAdapter,
            IOrderByAdapter orderByAdapter)
        {
            _filterByAdapter = filterByAdapter;
            _orderByAdapter = orderByAdapter;
        }

        public PageRequestAdapter()
            : this(new FilterByAdapter(), new OrderByAdapter())
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
