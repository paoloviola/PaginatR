using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters.OrderBy
{
    public interface IOrderByDtoToModelAdapter
    {
        OrderByModel<TModel> ConvertToModel<TModel>(OrderByDto orderBy);
    }
}
