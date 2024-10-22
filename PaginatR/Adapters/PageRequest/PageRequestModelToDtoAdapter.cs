using PaginatR.Adapters.FilterBy;
using PaginatR.Adapters.OrderBy;
using PaginatR.Dtos;
using PaginatR.Models;
using System.Linq;

namespace PaginatR.Adapters.PageRequest
{
    public class PageRequestModelToDtoAdapter : IPageRequestModelToDtoAdapter
    {
        private readonly IFilterByExpressionToDtoAdapter _filterByAdapter;
        private readonly IOrderByModelToDtoAdapter _orderByAdapter;

        public PageRequestModelToDtoAdapter(
            IFilterByExpressionToDtoAdapter filterByAdapter,
            IOrderByModelToDtoAdapter orderByAdapter)
        {
            _filterByAdapter = filterByAdapter;
            _orderByAdapter = orderByAdapter;
        }

        public PageRequestModelToDtoAdapter()
            : this(new FilterByExpressionToDtoAdapter(), new OrderByModelToDtoAdapter())
        {
        }

        public PageRequestDto ConvertToDto<TModel>(PageRequestModel<TModel> pageRequest)
        {
            var filters = pageRequest.Filters
                .Select(filter => _filterByAdapter.ConvertToDto(filter));

            var orderings = pageRequest.Orderings
                .Select(ordering => _orderByAdapter.ConvertToDto(ordering));

            return new PageRequestDto(
                filters,
                orderings,
                pageRequest.PageNumber,
                pageRequest.PageSize);
        }
    }
}
