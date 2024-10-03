using System.Linq.Expressions;

namespace PaginatR.Extensions
{
    internal static class StringExtensions
    {
        public static Expression ToPropertyExpression(this string property, Expression parameter)
        {
            Expression propertyExpression = parameter;
            foreach (var currentProperty in property.Split('.'))
            {
                propertyExpression = Expression.Property(propertyExpression, currentProperty);
            }
            return propertyExpression;
        }
    }
}
