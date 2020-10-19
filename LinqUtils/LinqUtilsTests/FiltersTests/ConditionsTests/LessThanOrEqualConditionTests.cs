namespace csOdin.LinqUtils.Tests.FiltersTests.ConditionsTests
{
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters.Conditions;
    using System.Linq;
    using Xunit;

    public class LessThanOrEqualConditionTests
    {
        [Fact]
        public void DecimalLessThanOrEqualConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1985M;

            var people = DummyData.GetPeople().AsQueryable();

            var filterCondition = LessThanOrEqualCondition<Person>.Create(o => o.DecimalProperty, propertyValue1);

            var filter = filterCondition.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => i.DecimalProperty > propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.DecimalProperty <= propertyValue1).Should().NotBeEmpty();
        }

        [Fact]
        public void IntLessThanOrEqualConditionShouldReturnMatchingEntries()
        {
            var propertyValue1 = 1985;

            var people = DummyData.GetPeople().AsQueryable();

            var filterCondition = LessThanOrEqualCondition<Person>.Create(o => o.IntProperty, propertyValue1);

            var filter = filterCondition.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => i.DecimalProperty > propertyValue1).Should().BeEmpty();
            filteredPeople.Where(i => i.DecimalProperty <= propertyValue1).Should().NotBeEmpty();
        }
    }
}