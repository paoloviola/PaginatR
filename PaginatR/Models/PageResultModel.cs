using System.Collections.Generic;

namespace PaginatR.Models
{
    public class PageResultModel<TModel>
    {
        public IEnumerable<TModel> Content { get; set; }

        public long TotalCount { get; set; }

        public PageResultModel(IEnumerable<TModel> content, long totalCount)
        {
            Content = content;
            TotalCount = totalCount;
        }
    }
}
