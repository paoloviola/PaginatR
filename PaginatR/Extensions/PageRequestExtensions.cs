using PaginatR.Dtos;
using PaginatR.Models;
using Remote.Linq;
using Remote.Linq.Text.Json;
using System.Linq;
using System.Text.Json;

namespace PaginatR.Extensions
{
    public static class PageRequestExtensions
    {
        public static PageRequestDto MapToDto<TModel>(this PageRequestModel<TModel> pageRequest)
        {
            return new PageRequestDto(
                pageRequest.Filters.Select(filter => filter.ToRemoteLinqExpression()),
                pageRequest.Orderings.Select(ordering => ordering.MapToDto()),
                pageRequest.PageNumber,
                pageRequest.PageSize
            );
        }

        public static PageRequestModel<TModel> MapToModel<TModel>(this PageRequestDto pageRequest)
        {
            return new PageRequestModel<TModel>(
                pageRequest.Filters.Select(filter => filter.ToLinqExpression<TModel, bool>()),
                pageRequest.Orderings.Select(ordering => ordering.MapToModel<TModel>()),
                pageRequest.PageNumber,
                pageRequest.PageSize
            );
        }

        public static string SerializePageRequestToJson(this PageRequestDto pageRequest)
        {
            var options = new JsonSerializerOptions().ConfigureRemoteLinq();
            return JsonSerializer.Serialize(pageRequest, options);
        }

        public static PageRequestDto? DeserializePageRequestFromJson(this string json)
        {
            var options = new JsonSerializerOptions().ConfigureRemoteLinq();
            return JsonSerializer.Deserialize<PageRequestDto>(json, options);
        }
    }
}
