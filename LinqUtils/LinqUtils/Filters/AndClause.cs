namespace csOdin.LinqUtils.Filters
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class AndClause<T> : FilterClause<T>
    {
        private readonly List<OrClause<T>> _orClauses = new List<OrClause<T>>();

        public void Add(params OrClause<T>[] orClauses)
        {
            if (orClauses == null)
            {
                return;
            }

            _orClauses.AddRange(orClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            Expression andExpression = Expression.Constant(true);
            if (Conditions.IsNullOrempty() && _orClauses.IsNullOrempty() && Expressions.IsNullOrempty())
            {
                throw new FilterClauseWithoutConditionsException();
            }

            var andClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => andClauses.Add(condition.ToLinqExpression(parameter).Body));

            andClauses.ToList().ForEach(ac => andExpression = Expression.AndAlso(andExpression, ac));

            _orClauses.ForEach(oc => andExpression = Expression.AndAlso(andExpression, oc.ToLinqExpression(parameter).Body));

            Expressions.ForEach(ex => andExpression = Expression.AndAlso(andExpression, ex.Body));
            
            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}