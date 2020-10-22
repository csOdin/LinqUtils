namespace csOdin.LinqUtils.Tests.FiltersTests.ConditionsTests
{
    using csOdin.LinqUtils.Filters;
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters.Conditions;
    using System.Linq;
    using Xunit;

    public class GreaterThanConditionTests
    {
        [Fact]
        public void DecimalGreaterThanConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1985M;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = GreaterThanCondition<Person>.Create(o => o.DecimalProperty, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.DecimalProperty < propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.DecimalProperty > propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void DecimalGreaterThanNegatedConditionShouldReturnNotMatchingEntries()
        {
            var propertyValue1 = 1985M;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = GreaterThanCondition<Person>.Create(o => o.DecimalProperty, propertyValue1).Negate();
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.DecimalProperty < propertyValue1).Should().NotBeEmpty();
            filteredPeople.Where(i => i.DecimalProperty > propertyValue1).Should().BeEmpty();
        }

        [Fact]
        public void IntGreaterThanConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1985;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = GreaterThanCondition<Person>.Create(o => o.IntProperty, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.IntProperty < propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.IntProperty > propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void IntGreaterThanNegatedConditionShouldReturnNotMatchingEntries()
        {
            var propertyValue1 = 1985;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = GreaterThanCondition<Person>.Create(o => o.IntProperty, propertyValue1).Negate();
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.IntProperty < propertyValue1).Should().NotBeEmpty();
            filteredPeople.Where(i => i.IntProperty > propertyValue1).Should().BeEmpty();
        }
    }
}