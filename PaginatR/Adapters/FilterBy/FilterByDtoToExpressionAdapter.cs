using PaginatR.Dtos;
using PaginatR.Enums;
using PaginatR.Extensions;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PaginatR.Adapters.FilterBy
{
    internal class FilterByDtoToExpressionAdapter : IFilterByDtoToExpressionAdapter
    {
        public Expression<Func<TModel, bool>> ConvertToExpression<TModel>(FilterByDto filterBy)
        {
            var parameter = Expression.Parameter(typeof(TModel), "model");

            var filterProperty = filterBy.Property.ToPropertyExpression(parameter);
            var filterValue = Expression.Constant(filterBy.Value);

            var filterExpression = GetFilterExpression(filterProperty, filterValue, filterBy.Operation);
            return Expression.Lambda<Func<TModel, bool>>(filterExpression, parameter);
        }

        private static Expression GetFilterExpression(Expression property, Expression value, FilterOperation operation)
        {
            Expression filterExpression = operation switch
            {
                FilterOperation.Equals => Expression.Equal(property, value),
                FilterOperation.NotEquals => Expression.NotEqual(property, value),
                FilterOperation.GreaterThan => Expression.GreaterThan(property, value),
                FilterOperation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, value),
                FilterOperation.LessThan => Expression.LessThan(property, value),
                FilterOperation.LessThanOrEqual => Expression.LessThanOrEqual(property, value),
                FilterOperation.Contains => CreateCallOfComparisonMethod(GetContainsMethod(property.Type, value.Type), property, value),
                FilterOperation.NotContains => Expression.Not(CreateCallOfComparisonMethod(GetContainsMethod(property.Type, value.Type), property, value)),
                _ => throw new InvalidOperationException()
            };

            return filterExpression;
        }

        private static Expression CreateCallOfComparisonMethod(MethodInfo methodInfo, Expression property, Expression value)
        {
            return methodInfo.IsStatic ?
                Expression.Call(null, methodInfo, property, value) :
                Expression.Call(property, methodInfo, value);
        }

        private static MethodInfo GetContainsMethod(Type propertyType, Type valueType)
        {
            if (typeof(string).IsAssignableFrom(propertyType))
            {
                return typeof(string)
                    .GetMethod("Contains", new[] { typeof(string) });
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                return typeof(Enumerable)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(valueType);
            }

            throw new InvalidOperationException($"No contains method found for type {valueType}");
        }
    }
}
