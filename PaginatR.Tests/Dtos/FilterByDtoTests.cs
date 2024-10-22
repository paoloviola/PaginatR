using PaginatR.Dtos;
using PaginatR.Enums;
using System.Text.Json;

namespace PaginatR.Tests.Dtos;

[TestFixture]
public class FilterByDtoTests
{
    [Test]
    [TestCase(10)]
    [TestCase(10.0f)]
    [TestCase(10.0d)]
    [TestCase("Test")]
    [TestCase(true)]
    [TestCase(null)]
    public void Can_FilterByDto_ValuePropertyBeDeserialized(object? value)
    {
        // Arrange
        var filterByDto = new FilterByDto("Test", FilterOperation.Contains, value);
        var filterByJson = JsonSerializer.Serialize(filterByDto);
        
        // Act
        var result = JsonSerializer.Deserialize<FilterByDto>(filterByJson);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(value, Is.EqualTo(result.Value));
    }
}
