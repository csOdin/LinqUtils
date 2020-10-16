namespace csOdin.LinqUtils.Tests.FiltersTests
{
    using csOdin.LinqUtils.Tests.Models;
    using FluentAssertions;
    using LinqUtils.Filters;
    using System.Linq;
    using Xunit;

    public class OrClauseTest
    {
        [Fact]
        public void FilterByOrClauseWithAndConditionsShouldReturnMatchingValues()
        {
            var orPropertyValue1 = "Person";

            var andCityProperty = "City C";
            var andNameProperty = "Coder";

            var people = DummyData.GetPeople().AsQueryable();

            var andCondition1 = new Condition<Person>(o => o.Address.City, FilterOperators.Contains, andCityProperty);
            var andCondition2 = new Condition<Person>(o => o.Name, FilterOperators.Contains, andNameProperty);

            var andClause = new AndClause<Person>();
            andClause.Add(andCondition1, andCondition2);

            var condition1 = new Condition<Person>(o => o.Name, FilterOperators.Contains, orPropertyValue1);

            var orClause = new OrClause<Person>();
            orClause.Add(condition1);
            orClause.Add(andClause);

            var filter = orClause.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople
                .Where(i => !i.Name.Contains(orPropertyValue1) &&
                            !(i.Address.City.Contains(andCityProperty) && i.Name.Contains(andNameProperty))).Should().BeEmpty();
            filteredPeople
                .Where(i => i.Name.Contains(orPropertyValue1) ||
                            (i.Address.City.Contains(andCityProperty) && i.Name.Contains(andNameProperty))).Should().NotBeEmpty();
        }

        [Fact]
        public void FilterByOrClauseWithOnlyOrConditionsShouldReturnMatchingValues()
        {
            var propertyValue1 = "Person";
            var propertyValue2 = "User";

            var people = DummyData.GetPeople().AsQueryable();

            var condition1 = new Condition<Person>(o => o.Name, FilterOperators.Contains, propertyValue1);
            var condition2 = new Condition<Person>(o => o.Name, FilterOperators.Contains, propertyValue2);

            var orClause = new OrClause<Person>();
            orClause.Add(condition1, condition2);
            var filter = orClause.ToLinq();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !i.Name.Contains(propertyValue1) && !i.Name.Contains(propertyValue2)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1) || i.Name.Contains(propertyValue2)).Should().NotBeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1)).Should().NotBeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue2)).Should().NotBeEmpty();
        }

        [Fact]
        public void FilterByOrClauseWithoutconditionsShouldReturnException()
        {
            var people = DummyData.GetPeople().AsQueryable();
            var expectedcount = people.Count();

            var orClause = new OrClause<Person>();
            Assert.Throws<FilterClauseWithoutConditionsException>(() => orClause.ToLinq());
        }
    }
}