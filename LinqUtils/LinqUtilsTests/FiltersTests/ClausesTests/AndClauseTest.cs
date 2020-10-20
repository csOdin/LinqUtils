namespace csOdin.LinqUtils.Tests.FiltersTests.ClausesTests
{
    using csOdin.LinqUtils.Filters.Clauses;
    using csOdin.LinqUtils.Filters.Conditions;
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters;
    using System.Linq;
    using Xunit;

    public class AndClauseTest
    {
        [Fact]
        public void FilterByAndClauseWithOnlyAndConditionsShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";
            var propertyValue2 = "PersonSur 1";

            var people = DummyData.GetPeople().AsQueryable();

            var condition1 = ContainsCondition<Person>.Create(o => o.Name, propertyValue1);
            var condition2 = ContainsCondition<Person>.Create(o => o.Surname, propertyValue2);

            var filter = AndClause<Person>.Create(condition1, condition2);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !(i.Name.Contains(propertyValue1) && i.Surname.Contains(propertyValue2))).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1) && i.Surname.Contains(propertyValue2)).Should().NotBeEmpty();
        }

        [Fact]
        public void FilterByAndClauseWithOrConditionsShouldReturnMatchingValues()
        {
            var andPropertyValue = "Person";

            var orPropertyValue1 = "City B";
            var orPropertyValue2 = "City C";

            var people = DummyData.GetPeople().AsQueryable();

            var orCondition1 = ContainsCondition<Person>.Create(o => o.Address.City, orPropertyValue1);
            var orCondition2 = ContainsCondition<Person>.Create(o => o.Address.City, orPropertyValue2);
            var orClause = OrClause<Person>.Create(orCondition1, orCondition2);

            var andCondition = ContainsCondition<Person>.Create(o => o.Name, andPropertyValue);
            var filter = AndClause<Person>.Create(andCondition).Add(orClause);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !i.Name.Contains(andPropertyValue) ||
                                        (!i.Address.City.Contains(orPropertyValue1) && !i.Address.City.Contains(orPropertyValue2))).Should().BeEmpty();

            filteredPeople.Where(i => i.Name.Contains(andPropertyValue) &&
                                        (i.Address.City.Contains(orPropertyValue1) || i.Address.City.Contains(orPropertyValue2))).Should().NotBeEmpty();
        }
    }
}