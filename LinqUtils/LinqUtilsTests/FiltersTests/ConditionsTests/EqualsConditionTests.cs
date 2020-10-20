namespace csOdin.LinqUtils.Tests.FiltersTests.ConditionsTests
{
    using csOdin.LinqUtils.Filters;
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters.Conditions;
    using System.Linq;
    using Xunit;

    public class EqualsConditionTests
    {
        [Fact]
        public void BoolEqualsConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = false; ;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = EqualsCondition<Person>.Create(o => o.BoolProperty, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.BoolProperty != propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.BoolProperty == propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void DecimalEqualsConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1980.2M;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = EqualsCondition<Person>.Create(o => o.DecimalProperty, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.DecimalProperty != propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.DecimalProperty == propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void IntEqualsConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1980;

            var people = DummyData.GetPeople().AsQueryable();

            var filter = EqualsCondition<Person>.Create(o => o.IntProperty, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.IntProperty != propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.IntProperty == propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void StringEqualsConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = "Person 1 name";

            var people = DummyData.GetPeople().AsQueryable();

            var filter = EqualsCondition<Person>.Create(o => o.Name, propertyValue1);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => i.Name != propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.Name == propertyValue1).Should().NotBeEmpty();
        }
    }
}