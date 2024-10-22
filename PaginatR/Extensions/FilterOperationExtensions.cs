using PaginatR.Enums;
using System;

namespace PaginatR.Extensions
{
    public static class FilterOperationExtensions
    {
        public static FilterOperation Negate(this FilterOperation operation)
        {
            return operation switch
            {
                FilterOperation.Equals => FilterOperation.NotEquals,
                FilterOperation.NotEquals => FilterOperation.Equals,
                FilterOperation.GreaterThan => FilterOperation.LessThanOrEqual,
                FilterOperation.LessThan => FilterOperation.GreaterThanOrEqual,
                FilterOperation.GreaterThanOrEqual => FilterOperation.LessThan,
                FilterOperation.LessThanOrEqual => FilterOperation.GreaterThan,
                FilterOperation.Contains => FilterOperation.NotContains,
                FilterOperation.NotContains => FilterOperation.Contains,
                _ => throw new InvalidOperationException("Unsupported filter operation for negation.")
            };
        }
    }
}
