using PaginatR.Enums;
using PaginatR.Extensions;
using PaginatR.Models;
using PaginatR.Tests.Models;
using System.Linq.Expressions;

namespace PaginatR.Tests.Extensions
{
    [TestFixture]
    public class QueryableExtensionsTests
    {
        private IQueryable<AuthorModel> _authors;

        [SetUp]
        public void Setup()
        {
            _authors = new List<AuthorModel>
            {
                new AuthorModel("Test1Name", "Test1Email"),
                new AuthorModel("Test2Name", "Test2Email"),
                new AuthorModel("Test3Name", "Test3Email"),
                new AuthorModel("Test4Name", "Test4Email"),
                new AuthorModel("Test5Name", "Test5Email")
            }.AsQueryable();
        }

        [Test]
        public void ApplyPageRequest_ShouldApplyFiltersOrderingAndPaging()
        {
            // Arrange
            var filters = new List<Expression<Func<AuthorModel, bool>>>
            {
                x => x.Name == "Test3Name" || x.Name == "Test4Name" || x.Name == "Test5Name"
            };

            var orderings = new List<OrderByModel<AuthorModel>>
            {
                new OrderByModel<AuthorModel>(x => x.Name, OrderDirection.Descending)
            };

            var pageRequest = new PageRequestModel<AuthorModel>(filters, orderings, 0, 2);

            // Act
            var result = _authors.ApplyPageRequest(pageRequest);

            // Assert
            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.Content.Count(), Is.EqualTo(2));
            Assert.That(result.Content.First().Name, Is.EqualTo("Test5Name"));
        }

        [Test]
        public void ApplyPaging_ShouldReturnCorrectPage()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;

            // Act
            var pagedData = _authors.ApplyPaging(pageNumber, pageSize).ToList();

            // Assert
            Assert.That(pagedData.Count, Is.EqualTo(2));
            Assert.That(pagedData.First().Name, Is.EqualTo("Test3Name"));
        }

        [Test]
        public void ApplyOrdering_ShouldOrderByAscending()
        {
            // Arrange
            var orderings = new List<OrderByModel<AuthorModel>>
            {
                new OrderByModel<AuthorModel>(x => x.Name, OrderDirection.Ascending)
            };

            // Act
            var orderedData = _authors.ApplyOrdering(orderings).ToList();

            // Assert
            Assert.That(orderedData.First().Name, Is.EqualTo("Test1Name"));
            Assert.That(orderedData.Last().Name, Is.EqualTo("Test5Name"));
        }

        [Test]
        public void ApplyOrdering_ShouldOrderByDescending()
        {
            // Arrange
            var orderings = new List<OrderByModel<AuthorModel>>
            {
                new OrderByModel<AuthorModel>(x => x.Name, OrderDirection.Descending)
            };

            // Act
            var orderedData = _authors.ApplyOrdering(orderings).ToList();

            // Assert
            Assert.That(orderedData.First().Name, Is.EqualTo("Test5Name"));
            Assert.That(orderedData.Last().Name, Is.EqualTo("Test1Name"));
        }

        [Test]
        public void ApplyFilters_ShouldApplyMultipleFilters()
        {
            // Arrange
            var filters = new List<Expression<Func<AuthorModel, bool>>>
            {
                x => x.Name == "Test3Name" || x.Name == "Test4Name" || x.Name == "Test5Name",
                x => x.Name.Contains("Test")
            };

            // Act
            var filteredData = _authors.ApplyFilters(filters).ToList();

            // Assert
            Assert.That(filteredData.Count, Is.EqualTo(3));
            Assert.IsTrue(filteredData.All(x => x.Name.Contains("Test")));
        }

        [Test]
        public void ApplyFilters_ShouldApplyNoFilters()
        {
            // Arrange
            var filters = new List<Expression<Func<AuthorModel, bool>>>();

            // Act
            var filteredData = _authors.ApplyFilters(filters).ToList();

            // Assert
            Assert.That(filteredData.Count, Is.EqualTo(5));
        }
    }
}
