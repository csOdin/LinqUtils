namespace csOdin.LinqUtils.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class AndClause<T> : FilterClause<T>
    {
        public void Add(params OrClause<T>[] orClauses)
        {
            if (orClauses == null)
            {
                return;
            }

            _filterClauses.AddRange(orClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            ValidatecConditions();

            Expression andExpression = Expression.Constant(true);

            var andClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => andClauses.Add(condition.ToLinq(parameter).Body));

            andClauses.ToList().ForEach(ac => andExpression = Expression.AndAlso(andExpression, ac));

            _filterClauses.ForEach(oc => andExpression = Expression.AndAlso(andExpression, oc.ToLinqExpression(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}