using PaginatR.Adapters.OrderBy;
using PaginatR.Dtos;
using PaginatR.Enums;
using PaginatR.Models;
using PaginatR.Tests.Models;

namespace PaginatR.Tests.Adapters.OrderBy;

[TestFixture]
public class OrderByDtoToModelAdapterTests
{
    [Test]
    public void Can_ConvertToModel_ConvertToProperModelUsingPath()
    {
        // Arrange
        var property = $"{nameof(BookModel.Author)}.{nameof(AuthorModel.Name)}";

        var adapter = new OrderByDtoToModelAdapter();
        var inputDto = new OrderByDto(property, OrderDirection.Ascending);
        var expected = new OrderByModel<BookModel>(model => model.Author.Name, OrderDirection.Ascending);

        // Act
        var actual = adapter.ConvertToModel<BookModel>(inputDto);

        // Assert
        Assert.That(actual.Property.ToString(), Is.EqualTo(expected.Property.ToString()));
        Assert.That(actual.Direction, Is.EqualTo(expected.Direction));
    }

    [Test]
    public void Can_ConvertToModel_ConvertToProperModelUsingValueType()
    {
        // Arrange
        var property = $"{nameof(BookModel.Reviews)}";

        var adapter = new OrderByDtoToModelAdapter();
        var inputDto = new OrderByDto(property, OrderDirection.Ascending);
        var expected = new OrderByModel<BookModel>(model => model.Reviews, OrderDirection.Ascending);

        // Act
        var actual = adapter.ConvertToModel<BookModel>(inputDto);

        // Assert
        Assert.That(actual.Property.ToString(), Is.EqualTo(expected.Property.ToString()));
        Assert.That(actual.Direction, Is.EqualTo(expected.Direction));
    }
}
