namespace csOdin.LinqUtils.Tests.FiltersTests.ConditionsTests
{
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters.Conditions;
    using System.Linq;
    using Xunit;

    public class ContainsConditionTests
    {
        [Fact]
        public void FilterByLambdaConditionShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";

            var people = DummyData.GetPeople().AsQueryable();

            var filterCondition = ContainsCondition<Person>.Create(o => o.Name, propertyValue1);

            var filter = filterCondition.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => !i.Name.Contains(propertyValue1)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1)).Should().NotBeEmpty();
        }

        [Fact]
        public void FilterByStringConditionShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";

            var people = DummyData.GetPeople().AsQueryable();

            var filterCondition = ContainsCondition<Person>.Create("Name", propertyValue1);

            var filter = filterCondition.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => !i.Name.Contains(propertyValue1)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1)).Should().NotBeEmpty();
        }
    }
}