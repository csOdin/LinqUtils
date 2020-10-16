namespace csOdin.LinqUtils.Filters
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class OrClause<T> : FilterClause<T>
    {
        private readonly List<AndClause<T>> _andClauses = new List<AndClause<T>>();

        public void Add(params AndClause<T>[] andClauses)
        {
            if (andClauses == null)
            {
                return;
            }

            _andClauses.AddRange(andClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            Expression orExpression = Expression.Constant(false);
            if (Conditions.IsNullOrempty() && _andClauses.IsNullOrempty() && Expressions.IsNullOrempty())
            {
                throw new FilterClauseWithoutConditionsException();
            }

            var orClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => orClauses.Add(condition.ToLinqExpression(parameter).Body));

            orClauses.ToList().ForEach(oc => orExpression = Expression.OrElse(orExpression, oc));

            _andClauses.ForEach(ac => orExpression = Expression.OrElse(orExpression, ac.ToLinqExpression(parameter).Body));

            Expressions.ForEach(ex => orExpression = Expression.OrElse(orExpression, ex.Body));

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}