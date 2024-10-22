using PaginatR.Adapters.FilterBy;
using PaginatR.Enums;
using PaginatR.Tests.Models;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace PaginatR.Tests.Adapters.FilterBy;

[TestFixture]
public class FilterByExpressionToDtoAdapterTests
{
    [Test]
    [TestCase("model => (model.Author.Name == \"Test\")", "Author.Name", FilterOperation.Equals, "Test")]
    [TestCase("model => (model.Author.Name != \"Test\")", "Author.Name", FilterOperation.NotEquals, "Test")]
    [TestCase("model => model.Author.Name.Contains(\"Test\")", "Author.Name", FilterOperation.Contains, "Test")]
    [TestCase("model => Not(model.Author.Name.Contains(\"Test\"))", "Author.Name", FilterOperation.NotContains, "Test")]
    public void Can_ConvertExpressionToDto_ConvertStringExpressions(string expressionString,
        string expectedProperty, FilterOperation expectedOperation, string expectedValue)
    {
        // Arrange
        var adapter = new FilterByExpressionToDtoAdapter();
        var expression = ParseExpression<BookModel>(expressionString);

        // Act
        var dto = adapter.ConvertToDto(expression);

        // Assert
        Assert.That(dto.Property, Is.EqualTo(expectedProperty));
        Assert.That(dto.Operation, Is.EqualTo(expectedOperation));
        Assert.That(dto.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    [TestCase("model => (model.Author.Name == null)", "Author.Name", FilterOperation.Equals, null)]
    [TestCase("model => (model.Author.Name != null)", "Author.Name", FilterOperation.NotEquals, null)]
    public void Can_ConvertExpressionToDto_ConvertStringNullExpressions(string expressionString,
        string expectedProperty, FilterOperation expectedOperation, string? expectedValue)
    {
        // Arrange
        var adapter = new FilterByExpressionToDtoAdapter();
        var expression = ParseExpression<BookModel>(expressionString);

        // Act
        var dto = adapter.ConvertToDto(expression);

        // Assert
        Assert.That(dto.Property, Is.EqualTo(expectedProperty));
        Assert.That(dto.Operation, Is.EqualTo(expectedOperation));
        Assert.That(dto.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    [TestCase("model => (model.Reviews > 10)", "Reviews", FilterOperation.GreaterThan, 10)]
    [TestCase("model => (model.Reviews >= 10)", "Reviews", FilterOperation.GreaterThanOrEqual, 10)]
    [TestCase("model => (model.Reviews < 10)", "Reviews", FilterOperation.LessThan, 10)]
    [TestCase("model => (model.Reviews <= 10)", "Reviews", FilterOperation.LessThanOrEqual, 10)]
    public void Can_ConvertExpressionToDto_ConvertNumericExpressions(string expressionString,
        string expectedProperty, FilterOperation expectedOperation, object expectedValue)
    {
        // Arrange
        var adapter = new FilterByExpressionToDtoAdapter();
        var expression = ParseExpression<BookModel>(expressionString);

        // Act
        var dto = adapter.ConvertToDto(expression);

        // Assert
        Assert.That(dto.Property, Is.EqualTo(expectedProperty));
        Assert.That(dto.Operation, Is.EqualTo(expectedOperation));
        Assert.That(dto.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    [TestCase("model => model.Chapters.Contains(\"Test\")", "Chapters", FilterOperation.Contains, "Test")]
    [TestCase("model => Not(model.Chapters.Contains(\"Test\"))", "Chapters", FilterOperation.NotContains, "Test")]
    public void Can_ConvertExpressionToDto_ConvertEnumerationExpressions(string expressionString,
        string expectedProperty, FilterOperation expectedOperation, string expectedValue)
    {
        // Arrange
        var adapter = new FilterByExpressionToDtoAdapter();
        var expression = ParseExpression<BookModel>(expressionString);

        // Act
        var dto = adapter.ConvertToDto(expression);

        // Assert
        Assert.That(dto.Property, Is.EqualTo(expectedProperty));
        Assert.That(dto.Operation, Is.EqualTo(expectedOperation));
        Assert.That(dto.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    public void Can_ConvertExpressionToDto_ConvertEnumerationNullExpressions()
    {
        // Arrange
        var adapter = new FilterByExpressionToDtoAdapter();

        // Act
        var dto = adapter.ConvertToDto<BookModel>(model => model.Chapters.Contains(null));

        // Assert
        Assert.That(dto.Property, Is.EqualTo(nameof(BookModel.Chapters)));
        Assert.That(dto.Operation, Is.EqualTo(FilterOperation.Contains));
        Assert.That(dto.Value, Is.Null);
    }

    private Expression<Func<TModel, bool>> ParseExpression<TModel>(string expressionString)
    {
        var expression = DynamicExpressionParser.ParseLambda(typeof(TModel), typeof(bool), expressionString);
        return (Expression<Func<TModel, bool>>)expression;
    }
}