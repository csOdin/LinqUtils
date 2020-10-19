namespace csOdin.LinqUtils.Filters.Clauses
{
    using csOdin.LinqUtils.Extensions;
    using csOdin.LinqUtils.Filters.Conditions;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class FilterClause<T>
    {
        protected readonly List<FilterClause<T>> _filterClauses = new List<FilterClause<T>>();

        public FilterClause() => Conditions = new List<Condition<T>>();

        protected List<Condition<T>> Conditions { get; }

        public void Add(params Condition<T>[] conditions)
        {
            if (conditions == null)
            {
                return;
            }
            Conditions.AddRange(conditions);
        }

        public Expression<Func<T, bool>> ToLinq()
        {
            var parameter = Expression.Parameter(typeof(T));
            return ToLinq(parameter);
        }

        public abstract Expression<Func<T, bool>> ToLinq(ParameterExpression parameter);

        protected void ValidatecConditions()
        {
            if (Conditions.IsNullOrempty() && _filterClauses.IsNullOrempty())
            {
                throw new FilterClauseWithoutConditionsException();
            }
        }
    }
}