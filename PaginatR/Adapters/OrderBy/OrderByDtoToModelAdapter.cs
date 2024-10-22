﻿using System.Linq.Expressions;
using System;
using PaginatR.Dtos;
using PaginatR.Models;
using PaginatR.Extensions;

namespace PaginatR.Adapters.OrderBy
{
    internal class OrderByDtoToModelAdapter : IOrderByDtoToModelAdapter
    {
        public OrderByModel<TModel> ConvertToModel<TModel>(OrderByDto orderBy)
        {
            var property = GetExpressionOfPropertyName<TModel>(orderBy.Property);
            return new OrderByModel<TModel>(property, orderBy.Direction);
        }

        private static Expression<Func<TModel, object>> GetExpressionOfPropertyName<TModel>(string property)
        {
            var parameter = Expression.Parameter(typeof(TModel), "model");
            Expression propertyExpression = property.ToPropertyExpression(parameter);

            if (propertyExpression.Type.IsValueType)
            {
                propertyExpression = Expression.Convert(propertyExpression, typeof(object));
            }

            return Expression.Lambda<Func<TModel, object>>(propertyExpression, parameter);
        }
    }
}