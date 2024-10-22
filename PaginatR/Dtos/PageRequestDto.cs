using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PaginatR.Dtos
{
    public class PageRequestDto
    {
        public IEnumerable<FilterByDto> Filters { get; set; }

        public IEnumerable<OrderByDto> Orderings { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        [JsonConstructor]
        public PageRequestDto(
            IEnumerable<FilterByDto> filters,
            IEnumerable<OrderByDto> orderings,
            int pageNumber,
            int pageSize)
        {
            Filters = filters;
            Orderings = orderings;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PageRequestDto(int pageNumber, int pageSize)
            : this(Enumerable.Empty<FilterByDto>(), Enumerable.Empty<OrderByDto>(), pageNumber, pageSize)
        {
        }
    }
}
