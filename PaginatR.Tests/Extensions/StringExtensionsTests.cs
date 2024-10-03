using PaginatR.Extensions;
using System.Linq.Expressions;
using PaginatR.Tests.Models;

namespace PaginatR.Tests.Extensions;

[TestFixture]
public class StringExtensionsTests
{
    [Test]
    public void Can_ToPropertyExpression_GetPropertyInPath()
    {
        // Arrange
        var property = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Email)}";
        var parameter = Expression.Parameter(typeof(BookModel), "model");

        var expected = Expression.Property(
            Expression.Property(parameter, nameof(BookModel.Author)),
            nameof(AuthorModel.Email)
        );

        // Act
        var actual = property.ToPropertyExpression(parameter);

        // Assert
        Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
    }
}
