namespace csOdin.LinqUtils.Filters
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class OrClause<T> : FilterClause<T>
    {
        public void Add(params AndClause<T>[] andClauses)
        {
            if (andClauses == null)
            {
                return;
            }

            _filterClauses.AddRange(andClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            ValidatecConditions();

            Expression orExpression = Expression.Constant(false);

            var orClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => orClauses.Add(condition.ToLinq(parameter).Body));

            orClauses.ToList().ForEach(oc => orExpression = Expression.OrElse(orExpression, oc));

            _filterClauses.ForEach(ac => orExpression = Expression.OrElse(orExpression, ac.ToLinqExpression(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}