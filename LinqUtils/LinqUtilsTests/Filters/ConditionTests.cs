namespace csOdin.LinqUtils.Tests.FiltersTests
{
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters;
    using System.Linq;
    using Xunit;

    public class ConditionTests
    {
        [Fact]
        public void FilterByConditionShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";

            var people = DummyData.GetPeople().AsQueryable();

            var filterCondition = new Condition<Person>(o => o.Name, FilterOperators.Contains, propertyValue1);

            var filter = filterCondition.ToLinqExpression();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => !i.Name.Contains(propertyValue1)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1)).Should().NotBeEmpty();
        }
    }
}