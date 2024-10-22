using System.Collections.Generic;

namespace PaginatR.Dtos
{
    public class PageResultDto<TModel>
    {
        public IEnumerable<TModel> Content { get; set; }

        public long TotalCount { get; set; }

        public PageResultDto(IEnumerable<TModel> content, long totalCount)
        {
            Content = content;
            TotalCount = totalCount;
        }
    }
}
