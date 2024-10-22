using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters.OrderBy
{
    internal interface IOrderByModelToDtoAdapter
    {
        OrderByDto ConvertToDto<TModel>(OrderByModel<TModel> orderBy);
    }
}
