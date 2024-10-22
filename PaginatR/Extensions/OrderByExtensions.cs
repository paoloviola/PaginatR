using PaginatR.Dtos;
using PaginatR.Models;
using Remote.Linq;

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
    }
}
