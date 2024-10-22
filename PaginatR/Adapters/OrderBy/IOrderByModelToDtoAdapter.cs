using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters.OrderBy
{
    public interface IOrderByModelToDtoAdapter
    {
        OrderByDto ConvertToDto<TModel>(OrderByModel<TModel> orderBy);
    }
}
