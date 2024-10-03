using PaginatR.Enums;

namespace PaginatR.Dtos
{
    public class FilterByDto
    {
        public string Property { get; set; }

        public FilterOperation Operation { get; set; }

        public object? Value { get; set; }

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
