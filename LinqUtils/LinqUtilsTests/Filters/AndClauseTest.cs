﻿namespace csOdin.LinqUtilsTests.FiltersTests
{
    using FluentAssertions;
    using LinqUtils.Filters;
    using LinqUtilsTests.Models;
    using System.Linq;
    using Xunit;

    public class AndClauseTest
    {
        [Fact]
        public void AndClauseWithOnlyAndConditions()
        {
            var propertyValue1 = "Person";
            var propertyValue2 = "PersonSur 1";

            var people = DummyData.GetPeople().AsQueryable();

            var condition1 = new Condition<Person>(o => o.Name, FilterOperators.Contains, propertyValue1);
            var condition2 = new Condition<Person>(o => o.Surname, FilterOperators.Contains, propertyValue2);

            var andClause = new AndClause<Person>();
            andClause.Add(condition1, condition2);
            var filter = andClause.ToLinqExpression();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();
            filteredPeople.Where(i => !(i.Name.Contains(propertyValue1) && i.Surname.Contains(propertyValue2))).Should().BeEmpty();
            filteredPeople.Where(i => i.Name.Contains(propertyValue1) && i.Surname.Contains(propertyValue2)).Should().NotBeEmpty();
        }

        [Fact]
        public void AndClauseWithOrConditions()
        {
            var andPropertyValue = "Person";

            var orPropertyValue1 = "City B";
            var orPropertyValue2 = "City C";

            var people = DummyData.GetPeople().AsQueryable();

            var andCondition = new Condition<Person>(o => o.Name, FilterOperators.Contains, andPropertyValue);

            var orCondition1 = new Condition<Person>(o => o.Address.City, FilterOperators.Contains, orPropertyValue1);

            var orCondition2 = new Condition<Person>(o => o.Address.City, FilterOperators.Contains, orPropertyValue2);

            var orClause = new OrClause<Person>();
            orClause.Add(orCondition1, orCondition2);

            var andClause = new AndClause<Person>();
            andClause.Add(andCondition);
            andClause.Add(orClause);

            var filter = andClause.ToLinqExpression();

            var filteredPeople = people.Where(filter);
            filteredPeople.Should().NotBeNull();

            filteredPeople.Where(i => !i.Name.Contains(andPropertyValue) ||
                                        (!i.Address.City.Contains(orPropertyValue1) && !i.Address.City.Contains(orPropertyValue2))).Should().BeEmpty();

            filteredPeople.Where(i => i.Name.Contains(andPropertyValue) &&
                                        (i.Address.City.Contains(orPropertyValue1) || i.Address.City.Contains(orPropertyValue2))).Should().NotBeEmpty();
        }
    }
}