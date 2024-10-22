using PaginatR.Enums;
using Remote.Linq.Expressions;
using System.Text.Json.Serialization;

namespace PaginatR.Dtos
{
    public class OrderByDto
    {
        public LambdaExpression Property { get; set; }

        public OrderDirection Direction { get; set; }

        [JsonConstructor]
        public OrderByDto(LambdaExpression property, OrderDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
