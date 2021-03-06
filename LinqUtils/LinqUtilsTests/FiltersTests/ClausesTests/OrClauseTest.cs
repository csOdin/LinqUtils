﻿namespace csOdin.LinqUtils.Tests.FiltersTests.ClausesTests
{
    using csOdin.LinqUtils.Filters.Clauses;
    using csOdin.LinqUtils.Filters.Conditions;
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

            var andCondition1 = ContainsCondition<Person>.Create(o => o.Address.City, andCityProperty);
            var andCondition2 = ContainsCondition<Person>.Create(o => o.Name, andNameProperty);

            var andClause = AndClause<Person>.Create(andCondition1, andCondition2);

            var condition1 = ContainsCondition<Person>.Create(o => o.Name, orPropertyValue1);

            var filter = OrClause<Person>.Create(condition1).Add(andClause);
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

            var condition1 = ContainsCondition<Person>.Create(o => o.Name, propertyValue1);
            var condition2 = ContainsCondition<Person>.Create(o => o.Name, propertyValue2);

            var filter = OrClause<Person>.Create(condition1, condition2);
            var filteredPeople = people.Where(filter);

            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !i.Name.Contains(propertyValue1) && !i.Name.Contains(propertyValue2)).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1) || i.Name.Contains(propertyValue2)).Should().NotBeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1)).Should().NotBeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue2)).Should().NotBeEmpty();
        }
    }
}