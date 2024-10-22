using PaginatR.Enums;
using Remote.Linq.Expressions;

namespace PaginatR.Dtos
{
    public class OrderByDto
    {
        public LambdaExpression Property { get; set; }

        public OrderDirection Direction { get; set; }

        public OrderByDto(LambdaExpression property, OrderDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
