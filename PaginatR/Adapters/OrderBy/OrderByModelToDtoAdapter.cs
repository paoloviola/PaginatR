using PaginatR.Dtos;
using PaginatR.Extensions;
using PaginatR.Models;
using System;
using System.Linq.Expressions;

namespace PaginatR.Adapters.OrderBy
{
    internal class OrderByModelToDtoAdapter : IOrderByModelToDtoAdapter
    {
        public OrderByDto ConvertToDto<TModel>(OrderByModel<TModel> orderBy)
        {
            var property = GetPropertyNameOfExpression(orderBy.Property);
            return new OrderByDto(property, orderBy.Direction);
        }

        private static string GetPropertyNameOfExpression<TModel>(Expression<Func<TModel, object>> expression)
        {
            var extractedExpression = ExtractOptionalConvertExpression(expression.Body);

            var extractedMemberExpression = extractedExpression as MemberExpression
                ?? throw new InvalidOperationException("Expression must point to a valid property or field.");

            return extractedMemberExpression.ToPropertyPath();
        }

        private static Expression ExtractOptionalConvertExpression(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression
                && unaryExpression.NodeType == ExpressionType.Convert)
            {
                return unaryExpression.Operand;
            }

            return expression;
        }
    }
}
