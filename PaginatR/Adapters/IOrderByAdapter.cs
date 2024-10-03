using PaginatR.Dtos;
using PaginatR.Models;

namespace PaginatR.Adapters
{
    public interface IOrderByAdapter
    {
        OrderByModel<TModel> ConvertToModel<TModel>(OrderByDto orderBy);
    }
}
