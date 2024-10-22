using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PaginatR.Dtos
{
    public class PageResultDto<TModel>
    {
        public IEnumerable<TModel> Content { get; set; }

        public long TotalCount { get; set; }

        [JsonConstructor]
        public PageResultDto(IEnumerable<TModel> content, long totalCount)
        {
            Content = content;
            TotalCount = totalCount;
        }
    }
}
