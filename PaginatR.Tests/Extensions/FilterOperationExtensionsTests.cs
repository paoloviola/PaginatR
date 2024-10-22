using PaginatR.Enums;
using PaginatR.Extensions;

namespace PaginatR.Tests.Extensions;

[TestFixture]
public class FilterOperationExtensionsTests
{
    [Test]
    [TestCase(FilterOperation.Equals, ExpectedResult = FilterOperation.NotEquals)]
    [TestCase(FilterOperation.NotEquals, ExpectedResult = FilterOperation.Equals)]
    [TestCase(FilterOperation.GreaterThan, ExpectedResult = FilterOperation.LessThanOrEqual)]
    [TestCase(FilterOperation.GreaterThanOrEqual, ExpectedResult = FilterOperation.LessThan)]
    [TestCase(FilterOperation.LessThan, ExpectedResult = FilterOperation.GreaterThanOrEqual)]
    [TestCase(FilterOperation.LessThanOrEqual, ExpectedResult = FilterOperation.GreaterThan)]
    [TestCase(FilterOperation.Contains, ExpectedResult = FilterOperation.NotContains)]
    [TestCase(FilterOperation.NotContains, ExpectedResult = FilterOperation.Contains)]
    public FilterOperation Can_Negate_FilterOperation(FilterOperation filterOperation)
    {
        return filterOperation.Negate();
    }
}
