using PaginatR.Dtos;
using PaginatR.Models;
using Remote.Linq;
using System.Linq;

namespace PaginatR.Extensions
{
    public static class PageRequestExtensions
    {
        public static PageRequestDto MapToDto<TModel>(this PageRequestModel<TModel> request)
        {
            return new PageRequestDto(
                request.Filters.Select(filter => filter.ToRemoteLinqExpression()),
                request.Orderings.Select(ordering => ordering.MapToDto()),
                request.PageNumber,
                request.PageSize
            );
        }

        public static PageRequestModel<TModel> MapToModel<TModel>(PageRequestDto request)
        {
            return new PageRequestModel<TModel>(
                request.Filters.Select(filter => filter.ToLinqExpression<TModel, bool>()),
                request.Orderings.Select(ordering => ordering.MapToModel<TModel>()),
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
