using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaginatR.Extensions
{
    internal static class ExpressionExtensions
    {
        public static string ToPropertyPath(this MemberExpression expression)
        {
            var propertyPath = new List<string>();

            MemberExpression? currentExpression = expression;
            while (currentExpression != null)
            {
                propertyPath.Insert(0, currentExpression.Member.Name);
                currentExpression = currentExpression.Expression as MemberExpression;
            }

            return string.Join(".", propertyPath);
        }

        public static string? GetComparingProperty(this Expression expression)
        {
            expression = expression.UnwrapLambda();

            MemberExpression? comparingProperty = null;
            if (expression is BinaryExpression binaryExpression)
            {
                comparingProperty = binaryExpression.Left as MemberExpression;
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                var @object = methodCallExpression.Object ?? methodCallExpression.Arguments[0];
                comparingProperty = @object as MemberExpression;
            }
            else if (expression is UnaryExpression unaryExpression)
            {
                return GetComparingProperty(unaryExpression.Operand);
            }

            return comparingProperty?.ToPropertyPath();
        }

        public static object? GetComparingValue(this Expression expression)
        {
            expression = expression.UnwrapLambda();

            ConstantExpression? comparingValue = null;
            if (expression is BinaryExpression binaryExpression)
            {
                comparingValue = binaryExpression.Right as ConstantExpression;
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                var @object = methodCallExpression.Object == null ?
                    methodCallExpression.Arguments[1] :
                    methodCallExpression.Arguments[0];

                comparingValue = @object as ConstantExpression;
            }
            else if (expression is UnaryExpression unaryExpression)
            {
                return GetComparingValue(unaryExpression.Operand);
            }

            if (comparingValue == null)
            {
                throw new InvalidOperationException("Expression is not a constant");
            }

            return comparingValue.Value;
        }

        private static Expression UnwrapLambda(this Expression expression)
        {
            while (expression is LambdaExpression lambda)
            {
                expression = lambda.Body;
            }
            return expression;
        }
    }
}
