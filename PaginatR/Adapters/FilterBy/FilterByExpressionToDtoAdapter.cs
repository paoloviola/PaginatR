using PaginatR.Dtos;
using PaginatR.Enums;
using PaginatR.Extensions;
using System;
using System.Linq.Expressions;

namespace PaginatR.Adapters.FilterBy
{
    internal class FilterByExpressionToDtoAdapter : IFilterByExpressionToDtoAdapter
    {
        public FilterByDto ConvertToDto<TModel>(Expression<Func<TModel, bool>> filterBy)
        {
            var property = filterBy.Body.GetComparingProperty()
                ?? throw new InvalidOperationException("No comparing property found in the filter expression.");

            var operation = ExtractFilterOperation(filterBy.Body);

            var value = filterBy.Body.GetComparingValue();

            return new FilterByDto(property, operation, value);
        }

        private static FilterOperation ExtractFilterOperation(Expression expression)
        {
            return expression switch
            {
                BinaryExpression binaryExpression => ExtractFilterOperation(binaryExpression),
                MethodCallExpression methodCallExpression => ExtractFilterOperation(methodCallExpression),

                UnaryExpression unaryExpression when unaryExpression.NodeType == ExpressionType.Not
                    => ExtractFilterOperation(unaryExpression.Operand).Negate(),

                _ => throw new InvalidOperationException($"Unsupported expression type: {expression.NodeType}")
            };
        }

        private static FilterOperation ExtractFilterOperation(MethodCallExpression methodCallExpression)
        {
            return methodCallExpression.Method.Name switch
            {
                "Contains" => FilterOperation.Contains,
                _ => throw new InvalidOperationException($"Unsupported method call: {methodCallExpression.Method.Name}")
            };
        }

        private static FilterOperation ExtractFilterOperation(BinaryExpression binaryExpression)
        {
            return binaryExpression.NodeType switch
            {
                ExpressionType.Equal => FilterOperation.Equals,
                ExpressionType.NotEqual => FilterOperation.NotEquals,
                ExpressionType.GreaterThan => FilterOperation.GreaterThan,
                ExpressionType.GreaterThanOrEqual => FilterOperation.GreaterThanOrEqual,
                ExpressionType.LessThan => FilterOperation.LessThan,
                ExpressionType.LessThanOrEqual => FilterOperation.LessThanOrEqual,
                _ => throw new InvalidOperationException($"Unsupported binary filter operation: {binaryExpression.NodeType}")
            };
        }
    }
}
