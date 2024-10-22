﻿using PaginatR.Dtos;
using System;
using System.Linq.Expressions;

namespace PaginatR.Adapters.FilterBy
{
    public interface IFilterByExpressionToDtoAdapter
    {
        FilterByDto ConvertToDto<TModel>(Expression<Func<TModel, bool>> filterBy);
    }
}
