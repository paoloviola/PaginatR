using PaginatR.Dtos;
using PaginatR.Models;
using Remote.Linq;
using Remote.Linq.Text.Json;
using System.Text.Json;

namespace PaginatR.Extensions
{
    public static class OrderByExtensions
    {
        public static OrderByDto MapToDto<TModel>(this OrderByModel<TModel> orderBy)
        {
            return new OrderByDto(
                orderBy.Property.ToRemoteLinqExpression(),
                orderBy.Direction
            );
        }

        public static OrderByModel<TModel> MapToModel<TModel>(this OrderByDto orderBy)
        {
            return new OrderByModel<TModel>(
                orderBy.Property.ToLinqExpression<TModel, object>(),
                orderBy.Direction
            );
        }

        public static string SerializeOrderByToJson(this OrderByDto orderBy)
        {
            var options = new JsonSerializerOptions().ConfigureRemoteLinq();
            return JsonSerializer.Serialize(orderBy, options);
        }

        public static OrderByDto? DeserializeOrderByFromJson(this string json)
        {
            var options = new JsonSerializerOptions().ConfigureRemoteLinq();
            return JsonSerializer.Deserialize<OrderByDto>(json, options);
        }
    }
}
