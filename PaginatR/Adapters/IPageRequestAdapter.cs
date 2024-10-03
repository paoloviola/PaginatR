using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters
{
    public interface IPageRequestAdapter
    {
        PageRequestModel<TModel> ConvertToModel<TModel>(PageRequestDto pageRequest);
    }
}
