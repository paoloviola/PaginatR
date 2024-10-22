using PaginatR.Enums;
using System.Text.Json.Serialization;

namespace PaginatR.Dtos
{
    public class OrderByDto
    {
        public string Property { get; set; }

        public OrderDirection Direction { get; set; }

        [JsonConstructor]
        public OrderByDto(string property, OrderDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
