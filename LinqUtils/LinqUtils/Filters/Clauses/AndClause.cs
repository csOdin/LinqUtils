﻿namespace csOdin.LinqUtils.Filters.Clauses
{
    using csOdin.LinqUtils.Filters.Conditions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class AndClause<T> : FilterClause<T>
    {
        private AndClause()
        {
        }

        private AndClause(params OrClause<T>[] orClauses) => Add(orClauses);

        private AndClause(params Condition<T>[] conditions) => Add(conditions);

        public static AndClause<T> Create() => new AndClause<T>();

        public static AndClause<T> Create(params Condition<T>[] conditions) => new AndClause<T>(conditions);

        public static AndClause<T> Create(params OrClause<T>[] orClauses) => new AndClause<T>(orClauses);

        public override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression andExpression = Expression.Constant(true);

            var andClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => andClauses.Add(condition.ToLinq(parameter).Body));

            andClauses.ToList().ForEach(ac => andExpression = Expression.AndAlso(andExpression, ac));

            _filterClauses.ForEach(oc => andExpression = Expression.AndAlso(andExpression, oc.ToLinq(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}