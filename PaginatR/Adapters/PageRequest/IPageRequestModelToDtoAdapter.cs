using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters.PageRequest
{
    internal interface IPageRequestModelToDtoAdapter
    {
        PageRequestDto ConvertToDto<TModel>(PageRequestModel<TModel> pageRequest);
    }
}
