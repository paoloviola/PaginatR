using PaginatR.Extensions;
using PaginatR.Tests.Models;
using System.Linq.Expressions;

namespace PaginatR.Tests.Extensions;

[TestFixture]
public class ExpressionExtensionsTests
{
    [Test]
    public void Can_ToPropertyPath_GetPropertyOfExpression()
    {
        // Arrange
        Expression<Func<BookModel, object>> expression = model => model.Author.Email;
        MemberExpression memberExpression = (MemberExpression)expression.Body;

        var expected = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Email)}";

        // Act
        var actual = memberExpression.ToPropertyPath();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingProperty_GetPropertyOfBinaryExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => model.Author.Name == "Test";
        var expected = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        // Act
        var actual = expression.GetComparingProperty();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingProperty_GetPropertyOfMethodCallExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => model.Author.Name.Contains("Test");
        var expected = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        // Act
        var actual = expression.GetComparingProperty();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingProperty_GetPropertyOfUnaryExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => !model.Author.Name.Contains("Test");
        var expected = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        // Act
        var actual = expression.GetComparingProperty();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingValue_GetValueOfBinaryExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => model.Author.Name == "Test";
        var expected = $"Test";

        // Act
        var actual = expression.GetComparingValue();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingValue_GetValueOfMethodCallExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => model.Author.Name.Contains("Test");
        var expected = $"Test";

        // Act
        var actual = expression.GetComparingValue();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingValue_GetValueOfUnaryExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => !model.Author.Name.Contains("Test");
        var expected = $"Test";

        // Act
        var actual = expression.GetComparingValue();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Can_GetComparingValue_GetValueOfNullExpression()
    {
        // Arrange
        Expression<Func<BookModel, bool>> expression = model => !model.Author.Name.Contains(null!);

        // Act
        var actual = expression.GetComparingValue();

        // Assert
        Assert.That(actual, Is.Null);
    }
}
