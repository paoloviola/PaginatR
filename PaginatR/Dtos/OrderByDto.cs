using PaginatR.Enums;

namespace PaginatR.Dtos
{
    public class OrderByDto
    {
        public string Property { get; set; }

        public OrderDirection Direction { get; set; }

        public OrderByDto(string property, OrderDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
