using PaginatR.Adapters;
using PaginatR.Dtos;
using PaginatR.Enums;
using PaginatR.Tests.Models;

namespace PaginatR.Tests.Adapters;

[TestFixture]
public class FilterByDtoToExpressionAdapterTests
{
    [Test]
    [TestCase(FilterOperation.Equals, ExpectedResult = "model => (model.Author.Name == \"Test\")")]
    [TestCase(FilterOperation.NotEquals, ExpectedResult = "model => (model.Author.Name != \"Test\")")]
    public string Can_ConvertToExpression_ConvertStringExpressions(FilterOperation operation)
    {
        // Arrange
        var property = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        var adapter = new FilterByAdapter();
        var inputDto = new FilterByDto(property, operation, "Test");

        // Act
        return adapter.ConvertToExpression<BookModel>(inputDto).ToString();
    }

    [Test]
    [TestCase(FilterOperation.GreaterThan, ExpectedResult = "model => (model.Reviews > 10)")]
    [TestCase(FilterOperation.GreaterThanOrEqual, ExpectedResult = "model => (model.Reviews >= 10)")]
    [TestCase(FilterOperation.LessThan, ExpectedResult = "model => (model.Reviews < 10)")]
    [TestCase(FilterOperation.LessThanOrEqual, ExpectedResult = "model => (model.Reviews <= 10)")]
    public string Can_ConvertToExpression_ConvertNumericExpressions(FilterOperation operation)
    {
        // Arrange
        var property = $"{nameof(BookModel.Reviews)}";

        var adapter = new FilterByAdapter();
        var inputDto = new FilterByDto(property, operation, 10);

        // Act
        return adapter.ConvertToExpression<BookModel>(inputDto).ToString();
    }

    [Test]
    [TestCase(FilterOperation.Contains, ExpectedResult = "model => model.Chapters.Contains(\"Test\")")]
    [TestCase(FilterOperation.NotContains, ExpectedResult = "model => Not(model.Chapters.Contains(\"Test\"))")]
    public string Can_ConvertToExpression_ConvertEnumerationExpressions(FilterOperation operation)
    {
        // Arrange
        var property = $"{nameof(BookModel.Chapters)}";

        var adapter = new FilterByAdapter();
        var inputDto = new FilterByDto(property, operation, "Test");

        // Act
        return adapter.ConvertToExpression<BookModel>(inputDto).ToString();
    }
}
