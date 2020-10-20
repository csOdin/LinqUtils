namespace csOdin.LinqUtils.Tests.OrderByTests
{
    using csOdin.LinqUtils.OrderBy;
    using csOdin.LinqUtils.OrderBy.Clauses;
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using System.Linq;
    using Xunit;

    public class OrderByTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void EmptyOrNullStringShouldReturnNull(string inputString)
        {
            var people = Models.DummyData.GetPeople().AsQueryable();

            var orderedPeople = people.OrderBy(inputString);

            orderedPeople.Should().BeNull();
        }

        [Fact]
        public void OrderByClauseShouldReturn()
        {
            var people = Models.DummyData.GetPeople().AsQueryable();

            var orderByClause = OrderByClause<Person>.Create()
                .AddAscending(o => o.Name)
                .AddDescending("Address.City")
                .AddAscending(o => o.IntProperty);

            var orderedPeople = people.OrderBy(orderByClause);

            orderedPeople.Expression.ToString().Should().Contain("OrderBy(x => x.Name).ThenByDescending(x => x.Address.City).ThenBy(x => x.IntProperty)");
        }

        [Fact]
        public void OrderByClauseShouldReturnNull()
        {
            var people = Models.DummyData.GetPeople().AsQueryable();

            var orderByClause = OrderByClause<Person>.Create();

            var orderedPeople = people.OrderBy(orderByClause);

            orderedPeople.Should().BeNull();
        }

        [Theory]
        [InlineData("Name", "OrderBy(x => x.Name)")]
        [InlineData("Name desc", "OrderByDescending(x => x.Name)")]
        [InlineData("Address.City, Name desc", "OrderBy(x => x.Address.City).ThenByDescending(x => x.Name)")]
        [InlineData("Address.City desc, Name", "OrderByDescending(x => x.Address.City).ThenBy(x => x.Name)")]
        [InlineData("Address.City desc, Name desc", "OrderByDescending(x => x.Address.City).ThenByDescending(x => x.Name)")]
        public void StringShouldReturn(string inputString, string expextedOrderByExpression)
        {
            var people = Models.DummyData.GetPeople().AsQueryable();

            var orderedPeople = people.OrderBy(inputString);

            orderedPeople.Expression.ToString().Should().Contain(expextedOrderByExpression);
        }
    }
}