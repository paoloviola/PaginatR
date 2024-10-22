using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters.PageRequest
{
    public interface IPageRequestDtoToModelAdapter
    {
        PageRequestModel<TModel> ConvertToModel<TModel>(PageRequestDto pageRequest);
    }
}
