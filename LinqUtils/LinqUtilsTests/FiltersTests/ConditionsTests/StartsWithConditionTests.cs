namespace csOdin.LinqUtils.Tests.FiltersTests.ConditionsTests
{
    using csOdin.LinqUtils.Filters;
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters.Conditions;
    using System.Linq;
    using Xunit;

    public class StartsWithConditionTests
    {
        [Fact]
        public void FilterByConditionShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";

            var people = DummyData.GetPeople().AsQueryable();

            var filter = StartsWithCondition<Person>.Create(o => o.Name, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !i.Name.StartsWith(propertyValue1)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.StartsWith(propertyValue1)).Should().NotBeEmpty();
        }

        [Fact]
        public void FilterByNegatedConditionShouldReturnNotMatchingValues()
        {
            var propertyValue1 = "Person";

            var people = DummyData.GetPeople().AsQueryable();

            var filter = StartsWithCondition<Person>.Create(o => o.Name, propertyValue1).Negate();
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !i.Name.StartsWith(propertyValue1)).Should().NotBeEmpty();
            filteredPeople.Where(i => i.Name.StartsWith(propertyValue1)).Should().BeEmpty();
        }
    }
}