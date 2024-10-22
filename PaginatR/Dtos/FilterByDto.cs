using PaginatR.Enums;
using System.Text.Json.Serialization;

namespace PaginatR.Dtos
{
    public class FilterByDto
    {
        public string Property { get; set; }

        public FilterOperation Operation { get; set; }

        public object? Value { get; set; }

        [JsonConstructor]
        public FilterByDto(
            string property,
            FilterOperation operation,
            object? value)
        {
            Property = property;
            Operation = operation;
            Value = value;
        }
    }
}
