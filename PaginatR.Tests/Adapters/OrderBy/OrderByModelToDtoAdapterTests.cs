using PaginatR.Adapters.OrderBy;
using PaginatR.Dtos;
using PaginatR.Enums;
using PaginatR.Models;
using PaginatR.Tests.Models;

namespace PaginatR.Tests.Adapters.OrderBy;

[TestFixture]
public class OrderByModelToDtoAdapterTests
{
    [Test]
    public void Can_ConvertToDto_ConvertToProperDtoUsingPath()
    {
        // Arrange
        var property = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        var adapter = new OrderByModelToDtoAdapter();
        var inputModel = new OrderByModel<BookModel>(model => model.Author.Name, OrderDirection.Ascending);
        var expected = new OrderByDto(property, OrderDirection.Ascending);

        // Act
        var actual = adapter.ConvertToDto(inputModel);

        // Assert
        Assert.That(expected.Property.ToString(), Is.EqualTo(actual.Property.ToString()));
        Assert.That(expected.Direction, Is.EqualTo(actual.Direction));
    }

    [Test]
    public void Can_ConvertToDto_ConvertToProperDtoUsingValueType()
    {
        // Arrange
        var property = $"{nameof(BookModel.Reviews)}";

        var adapter = new OrderByModelToDtoAdapter();
        var inputModel = new OrderByModel<BookModel>(model => model.Reviews, OrderDirection.Ascending);
        var expected = new OrderByDto(property, OrderDirection.Ascending);

        // Act
        var actual = adapter.ConvertToDto(inputModel);

        // Assert
        Assert.That(expected.Property.ToString(), Is.EqualTo(actual.Property.ToString()));
        Assert.That(expected.Direction, Is.EqualTo(actual.Direction));
    }
}
